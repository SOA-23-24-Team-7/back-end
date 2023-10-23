using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;
using FluentResults;
using System.Linq;

namespace Explorer.Tours.Core.UseCases;

public class TourService : CrudService<TourResponseDto, Tour>, ITourService
{
    private readonly ICrudRepository<Tour> _repository;
    private readonly IMapper _mapper;
    public TourService(ICrudRepository<Tour> repository, IMapper mapper) : base(repository, mapper) {
        _repository = repository;
        _mapper = mapper;
    }
    public Result<PagedResult<TourResponseDto>> GetAuthorsPagedTours(long authorId, int page, int pageSize)
    {
        var allTours = _repository.GetPaged(page, pageSize);
        var toursByAuthor = allTours.Results.Where(t => t.AuthorId == authorId).ToList();
        var pagedResult = new PagedResult<Tour>(toursByAuthor, toursByAuthor.Count);
        return MapToDto<TourResponseDto>(pagedResult);
    }
}
