using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class PersonDataBaseRepository : IPersonRepository
    {

        private readonly StakeholdersContext _dbContext;

        public PersonDataBaseRepository(StakeholdersContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Person? GetByUserId(long id)
        {
            var person = _dbContext.People.FirstOrDefault(person => person.UserId == id);
            return person;
        }
    }
}
