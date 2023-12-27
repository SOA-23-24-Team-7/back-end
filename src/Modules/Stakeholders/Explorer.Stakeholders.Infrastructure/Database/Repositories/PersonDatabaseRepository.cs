using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.Core.Domain.Problems;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class PersonDatabaseRepository : CrudDatabaseRepository<Person, StakeholdersContext>, IPersonRepository
    {

        private readonly StakeholdersContext _dbContext;
        private readonly DbSet<Person> _dbSet;

        public PersonDatabaseRepository(StakeholdersContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<Person>();
        }

        public Person? GetByUserId(long id)
        {
            var person = _dbContext.People.Include(x => x.User).FirstOrDefault(person => person.UserId == id);
            return person;
        }

        public Result<PagedResult<Person>> GetPagedByAdmin(int page, int pageSize, long adminId)
        {
            var task = _dbSet.Include(x => x.User).Where(x => x.UserId != adminId).GetPagedById(page, pageSize);
            task.Wait();
            return task.Result;
        }

        public Result<PagedResult<Person>> GetAll(int page, int pageSize)
        {
            var task = _dbContext.People.Include(x => x.User).GetPagedById(page, pageSize);
            task.Wait();
            return task.Result;
        }

        public bool ExistsByEmail(string email)
        {
            return _dbContext.People.Any(user => user.Email == email);
        }

        public Person GetByEmail(string email)
        {
            return _dbContext.People.FirstOrDefault(user => user.Email == email);
        }
    }
}
