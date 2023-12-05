using Explorer.Payments.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Public
{
    public interface IBundleService
    {
        Result<BundleResponseDto> Create(BundleCreationDto bundleDto, long authorId);
        Result<BundleResponseDto> Edit(long id, BundleEditDto bundleDto, long authorId);
        Result<BundleResponseDto> Publish(long id, long authorId);
        Result<BundleResponseDto> Archive(long id, long authorId);
        Result<BundleResponseDto> Delete(long id, long authorId);
    }
}
