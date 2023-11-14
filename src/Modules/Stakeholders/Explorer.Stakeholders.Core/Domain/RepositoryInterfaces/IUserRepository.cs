using Explorer.BuildingBlocks.Core.UseCases;
using FluentResults;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;

public interface IUserRepository
{
    bool Exists(string username);
    User? GetActiveByName(string username);
    User Create(User user);
    long GetPersonId(long userId);
    PagedResult<User> GetPagedByAdmin(int page, int pageSize, long adminId);
    public PagedResult<User> SearchUsers(int page, int pageSize, string searchUsername, long id);
}