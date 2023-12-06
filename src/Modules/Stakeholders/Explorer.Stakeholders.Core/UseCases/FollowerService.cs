using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.Problems;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Result<PagedResult<FollowerResponseWithUserDto>> GetFollowers(int page, int pageSize, long userId)
        {
            var result = _followerRepository.GetFollowersPagedById(page, pageSize, userId);

            var items = result.Results.Select(_mapper.Map<FollowerResponseWithUserDto>).ToList();
            items.ForEach(x => x.FollowedByPerson = _mapper.Map<PersonResponseDto>(_personRepository.GetByUserId(x.FollowedBy.Id)));

            return new PagedResult<FollowerResponseWithUserDto>(items, result.TotalCount);
        }
        public Result<PagedResult<FollowingResponseWithUserDto>> GetFollowings(int page, int pageSize, long userId)
        {
            var result = _followerRepository.GetFollowingsPagedById(page, pageSize, userId);

            var items = result.Results.Select(_mapper.Map<FollowingResponseWithUserDto>).ToList();
            items.ForEach(x => x.FollowingPerson = _mapper.Map<PersonResponseDto>(_personRepository.GetByUserId(x.Following.Id)));

            return new PagedResult<FollowingResponseWithUserDto>(items, result.TotalCount); ;
        }
    }
}
