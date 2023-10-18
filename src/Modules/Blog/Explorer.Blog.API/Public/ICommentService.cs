﻿using Explorer.Blog.API.Dtos;
using Explorer.BuildingBlocks.Core.UseCases;
using FluentResults;

namespace Explorer.Blog.API.Public
{
    public interface ICommentService
    {
        Result<CommentResponseDto> Create(CommentRequestDto commentData, long authorId);
        Result<PagedResult<CommentResponseDto>> GetPaged(int page, int pageSize);

    }
}
