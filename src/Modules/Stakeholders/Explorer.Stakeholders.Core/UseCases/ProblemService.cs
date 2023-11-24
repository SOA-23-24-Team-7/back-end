using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.Problems;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Utilities;
using Explorer.Tours.API.Internal;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class ProblemService : CrudService<ProblemResponseDto, Problem>, IProblemService
    {
        private readonly IProblemRepository _problemRepository;
        private readonly IInternalTourService _tourService;
        private readonly IProblemResolvingNotificationService _problemResolvingNotificationService;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public ProblemService(ICrudRepository<Problem> repository, IMapper mapper, IProblemRepository problemRepository, IInternalTourService tourService, IUserRepository userRepository, IProblemResolvingNotificationService problemResolvingNotificationService) : base(repository, mapper)
        {
            _problemRepository = problemRepository;
            _mapper = mapper;
            _tourService = tourService;
            _userRepository = userRepository;
            _problemResolvingNotificationService = problemResolvingNotificationService;
        }

        public Result ResolveProblem(long problemId)
        {
            try
            {
                Problem problem = CrudRepository.Get(problemId);
                problem.ResolveProblem();

                var result = CrudRepository.Update(problem);
                return Result.Ok();
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public Result<ProblemResponseDto> UpdateDeadline(long problemId, DateTime deadline, long adminID)
        {
            var problem = CrudRepository.Get(problemId);
            if (problem.Deadline == DateTime.MaxValue && problem.Deadline > DateTime.Now)
            {
                try
                {
                    problem.SetDeadline(deadline);
                    var result = CrudRepository.Update(problem);
                    var tourName = _tourService.GetToursName(problem.TourId);
                    var message = NotificationGenerator.GenerateDeadlineMessage(tourName, deadline);
                    var header = NotificationGenerator.GenerateDeadlineHeader(tourName);
                    var notification = new ProblemResolvingNotification(problem.Id, problem.Answer.AuthorId, adminID, message, DateTime.UtcNow, header);
                    _problemResolvingNotificationService.Create(notification);
                    return MapToDto<ProblemResponseDto>(result);
                }
                catch (KeyNotFoundException e)
                {
                    return Result.Fail(FailureCode.NotFound).WithError(e.Message);
                }
                catch (ArgumentException e)
                {
                    return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
                }
            }
            return Result.Fail(FailureCode.Forbidden).WithError("Deadline has already been set.");
        }

        public Result CreateAnswer(ProblemAnswerDto problemAnswer, long problemId)
        {
            try
            {
                var problem = _problemRepository.Get(problemId);
                if (problem.IsAnswered) throw new Exception();

                problem.CreateAnswer(problemAnswer.Answer, problemAnswer.AuthorId);
                CrudRepository.Update(problem);
                var header = NotificationGenerator.GenerateAnswerHeader(_tourService.GetToursName(problem.TourId));

                var notification = new ProblemResolvingNotification(problem.Id, problem.TouristId, problem.Answer.AuthorId, problemAnswer.Answer, DateTime.UtcNow, header);
                _problemResolvingNotificationService.Create(notification);
                return Result.Ok();
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public Result<ProblemCommentResponseDto> CreateComment(ProblemCommentCreateDto problemComment, long problemId)
        {
            try
            {
                var problem = _problemRepository.Get(problemId);
                if (!problem.IsAnswered) throw new Exception();

                var commenter = _userRepository.Get(problemComment.CommenterId);
                var comment = problem.CreateComment(problemComment.Text, commenter, problemComment.CommenterId);
                CrudRepository.Update(problem);

                long senderId = 0;
                long receiverId = 0;
                if (problemComment.CommenterId == problem.Answer.AuthorId)
                {
                    senderId = problem.Answer.AuthorId;
                    receiverId = problem.TouristId;
                }
                else
                {
                    senderId = problem.TouristId;
                    receiverId = problem.Answer.AuthorId;
                }

                var header = NotificationGenerator.GenerateCommentHeader(_tourService.GetToursName(problem.TourId));
                var notification = new ProblemResolvingNotification(problem.Id, receiverId, senderId, problemComment.Text, DateTime.UtcNow, header);
                _problemResolvingNotificationService.Create(notification);

                return _mapper.Map<ProblemCommentResponseDto>(comment);
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public Result<ProblemAnswerDto> GetAnswer(long problemId)
        {
            var problem = _problemRepository.Get(problemId);
            if (problem != null) return _mapper.Map<ProblemAnswerDto>(problem.Answer);

            return Result.Fail(FailureCode.InvalidArgument);
        }

        public Result<PagedResult<ProblemCommentResponseDto>> GetComments(long problemId)
        {
            var problem = _problemRepository.Get(problemId);
            if (problem != null)
            {
                var items = problem.Comments.Select(_mapper.Map<ProblemCommentResponseDto>).ToList();
                return new PagedResult<ProblemCommentResponseDto>(items, items.Count());
            }
            return Result.Fail(FailureCode.InvalidArgument);
        }

        public Result<PagedResult<ProblemResponseDto>> GetAll(int page, int pageSize)
        {
            var results = MapToDto<ProblemResponseDto>(_problemRepository.GetAll(page, pageSize));
            foreach (var problemDto in results.Value.Results)
            {
                problemDto.TourName = _tourService.GetToursName(problemDto.TourId);
                problemDto.TourAuthorId = _tourService.GetAuthorsId(problemDto.TourId);
            }

            return results;
        }

        public Result<ProblemResponseDto> Get(long id)
        {
            var result = MapToDto<ProblemResponseDto>(_problemRepository.Get(id));
            result.TourName = _tourService.GetToursName(result.TourId);
            result.TourAuthorId = _tourService.GetAuthorsId(result.TourId);
            return result;
        }

        public Result<PagedResult<ProblemResponseDto>> GetByUserId(int page, int pageSize, long id)
        {
            var results = MapToDto<ProblemResponseDto>(_problemRepository.GetByUserId(page, pageSize, id));
            foreach (var problemDto in results.Value.Results)
            {
                problemDto.TourName = _tourService.GetToursName(problemDto.TourId);
                problemDto.TourAuthorId = _tourService.GetAuthorsId(problemDto.TourId);
            }

            return results;
        }

        public Result<PagedResult<ProblemResponseDto>> GetByAuthor(int page, int pageSize, long id)
        {
            var results = MapToDto<ProblemResponseDto>(_problemRepository.GetByAuthor(page, pageSize, _tourService.GetAuthorsTours(id).ToList()));
            foreach (var problemDto in results.Value.Results)
            {
                problemDto.TourName = _tourService.GetToursName(problemDto.TourId);
                problemDto.TourAuthorId = _tourService.GetAuthorsId(problemDto.TourId);
            }

            return results;
        }
    }
}

