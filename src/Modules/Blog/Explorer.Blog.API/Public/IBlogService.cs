using Explorer.Blog.API.Dtos;
using Explorer.BuildingBlocks.Core.UseCases;
using FluentResults;

namespace Explorer.Blog.API.Public
{
    public enum VoteType { DOWNVOTE, UPVOTE }
    public interface IBlogService
    {
        Result<BlogResponseDto> Create<BlogCreateDto>(BlogCreateDto blog);
        Result<PagedResult<BlogResponseDto>> GetAll(int page, int pageSize);
        Result<BlogResponseDto> GetById(long id);
        Result SetVote(long blogId, long userId, VoteType voteType);
    }
}
