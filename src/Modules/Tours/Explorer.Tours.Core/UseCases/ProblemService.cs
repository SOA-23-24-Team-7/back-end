using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases
{
    public class ProblemService : CrudService<ProblemResponseDto, Problem>, IProblemService
{
        private readonly IProblemRepository _problemRepository;
    public ProblemService(ICrudRepository<Problem> repository, IMapper mapper) : base(repository, mapper) { }
        public ProblemService(ICrudRepository<Problem> repository, IMapper mapper,IProblemRepository problemRepository) : base(repository, mapper) {
            _problemRepository = problemRepository;
        }
        public Result<PagedResult<ProblemResponseDto>> GetByUserId(int page, int pageSize, int id)
        {
            return MapToDto<ProblemResponseDto>(_problemRepository.GetByUserId(page, pageSize, id));
        }
    }
}

