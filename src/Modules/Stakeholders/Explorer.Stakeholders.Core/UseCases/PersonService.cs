using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain.Problems;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class PersonService : CrudService<PersonResponseDto, Person>, IPersonService
    {

        private readonly IPersonRepository _personRepository;
        private readonly IUserService _userService;
        public PersonService(ICrudRepository<Person> repository, IUserService userService, IPersonRepository personRepository, IMapper mapper) : base(repository, mapper)
        {
            _personRepository = personRepository;
            _userService = userService;
        }

        public Result<PersonResponseDto> UpdatePerson(PersonUpdateDto personData)
        {
            try
            {
                var person = CrudRepository.Get(personData.Id);
                person.UpdatePerson(personData.Name, personData.Surname, personData.Bio, personData.Motto);
                _userService.UpdateProfilePicture(personData.UserId, personData.ProfilePicture);
                var result = CrudRepository.Update(person);
                return MapToDto<PersonResponseDto>(result);
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

        public Result<PersonResponseDto> GetByUserId(long id)
        {
            try
            {
                var result = _personRepository.GetByUserId(id);
                return MapToDto<PersonResponseDto>(result);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<PagedResult<PersonResponseDto>> GetPagedByAdmin(int page, int pageSize, long adminId)
        {
            return MapToDto<PersonResponseDto>(_personRepository.GetPagedByAdmin(page, pageSize, adminId));
        }

        public Result<PagedResult<PersonResponseDto>> GetAll(int page, int pageSize)
        {
            return MapToDto<PersonResponseDto>(_personRepository.GetAll(page, pageSize));
        }
    }
}
