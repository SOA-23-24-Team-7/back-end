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
        Result<int> Edit(BundleCreationDto bundleDto);
        Result<int> Publish(long id);
        Result<int> Archive(long id);
        Result<int> Delete(long id);
    }
}
