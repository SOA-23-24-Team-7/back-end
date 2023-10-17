using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public
{
    public interface IProblemService
    {
        Result<PagedResult<ProblemDto>> GetPaged(int page, int pageSize);
        Result<ProblemDto> Create(ProblemDto problem);
        Result<ProblemDto> Update(ProblemDto problem);
        Result Delete(int id);
    }
}
