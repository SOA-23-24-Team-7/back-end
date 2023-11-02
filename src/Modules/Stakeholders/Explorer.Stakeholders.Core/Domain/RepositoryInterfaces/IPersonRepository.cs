using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces
{
    public interface IPersonRepository
    {
        Person? GetByUserId(long id);
        Result<PagedResult<Person>> GetAll(int page, int pageSize);
    }
}
