using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System.Text.Json;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class FollowerService : CrudService<FollowerResponseDto, Follower>, IFollowerService
    {
        private readonly IFollowerRepository _followerRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        public FollowerService(ICrudRepository<Follower> repository, IFollowerRepository followerRepository, IMapper mapper, IPersonRepository personRepository) : base(repository, mapper)
        {
            _followerRepository = followerRepository;
            _personRepository = personRepository;
            _mapper = mapper;
        }

        public async Task<string> GetFollowers(string encodedIds, int page, int pageSize)
        {
            // Decode the JSON string to get the list of IDs
            var ids = JsonSerializer.Deserialize<List<long>>(encodedIds);

            // Convert the IDs to integers
            var userIds = ids.Select(id => (int)id).ToList();

            // Retrieve followers for the given user IDs
            var followers = new List<FollowerResponseWithUserDto>();
            foreach (var userId in userIds)
            {
                var follower = _personRepository.GetByUserId(userId);
                if (follower != null)
                {
                    var followerDto = _mapper.Map<FollowerResponseWithUserDto>(follower);
                    followerDto.FollowedByPerson = _mapper.Map<PersonResponseDto>(_personRepository.GetByUserId(followerDto.FollowedBy.Id));
                    followers.Add(followerDto);
                }
            }

            // Pagination logic
            var totalCount = followers.Count;
            var paginatedFollowers = followers.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            // Serialize the result to JSON string
            var jsonResult = JsonSerializer.Serialize(new PagedResult<FollowerResponseWithUserDto>(paginatedFollowers, totalCount));

            // Return the JSON string
            return jsonResult;
        }
        public async Task<string> GetFollowings(string encodedIds, int page, int pageSize)
        {

            // Decode the JSON string to get the list of IDs
            var ids = JsonSerializer.Deserialize<List<long>>(encodedIds);

            // Convert the IDs to integers
            var userIds = ids.Select(id => (int)id).ToList();

            // Retrieve followers for the given user IDs
            var followers = new List<FollowingResponseWithUserDto>();
            foreach (var userId in userIds)
            {
                var follower = _personRepository.GetByUserId(userId);
                if (follower != null)
                {
                    var followerDto = _mapper.Map<FollowingResponseWithUserDto>(follower);
                    followerDto.FollowingPerson = _mapper.Map<PersonResponseDto>(_personRepository.GetByUserId(followerDto.Following.Id));
                    followers.Add(followerDto);
                }
            }

            // Pagination logic
            var totalCount = followers.Count;
            var paginatedFollowers = followers.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            // Serialize the result to JSON string
            var jsonResult = JsonSerializer.Serialize(new PagedResult<FollowingResponseWithUserDto>(paginatedFollowers, totalCount));

            // Return the JSON string
            return jsonResult;
        }


    }
}
