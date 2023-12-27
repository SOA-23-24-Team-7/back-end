using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories;

public class UserDatabaseRepository : CrudDatabaseRepository<User, StakeholdersContext>, IUserRepository
{
    private readonly StakeholdersContext _dbContext;
    private readonly DbSet<User> _dbSet;

    public UserDatabaseRepository(StakeholdersContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<User>();
    }

    public User Get(long id)
    {
        var entity = _dbSet.Find(id);
        if (entity == null) throw new KeyNotFoundException("Not found: " + id);
        return entity;
    }

    public PagedResult<User> GetPagedByAdmin(int page, int pageSize, long adminId)
    {
        var task = _dbSet.Where(x => x.Id != adminId).GetPagedById(page, pageSize);
        task.Wait();
        return task.Result;
    }
    public PagedResult<User> SearchUsers(int page, int pageSize, string searchUsername, long id)
    {
        var task = _dbSet.Where(x => x.Username.ToLower().StartsWith(searchUsername.ToLower()) && x.Role != UserRole.Administrator && x.Id != id).GetPagedById(page, pageSize);
        task.Wait();
        return task.Result;
    }
    /*
    public Result<PagedResult<User>> GetPagedFollowersByUserId(int page, int pageSize, long userId)
    {
        var entity = _dbSet.Find(userId);
        if (entity == null) throw new KeyNotFoundException("Not found: " + userId);
        var a = entity.Followers;
        var task = _dbSet.Include(m => m.Followers).GetPagedById(pageSize, page);
        task.Wait();
        return task.Result;
    }
    */
    public bool Exists(string username)
    {
        return _dbContext.Users.Any(user => user.Username == username);
    }

    public User? GetActiveByName(string username)
    {
        return _dbContext.Users.FirstOrDefault(user => user.Username == username && user.IsActive);
    }

    public User Create(User user)
    {
        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();
        return user;
    }

    public long GetPersonId(long userId)
    {
        var person = _dbContext.People.FirstOrDefault(i => i.UserId == userId);
        if (person == null) throw new KeyNotFoundException("Not found.");
        return person.Id;
    }

    public string GetNameById(long id)
    {
        return _dbContext.Users.FirstOrDefault(u => u.Id == id).Username;
    }

    public string GetProfilePicture(long adminId)
    {
        return _dbSet.FirstOrDefault(x => x.Id == adminId).ProfilePicture;
    }

    public User GetByUsername(string username)
    {
        return _dbContext.Users.FirstOrDefault(user => user.Username == username);
    }

    public User EnableUser(long userId)
    {
        var user = _dbContext.Users.FirstOrDefault(user => user.Id == userId);
        user.IsActive = true;
        _dbContext.SaveChanges();
        return user;
    }
}