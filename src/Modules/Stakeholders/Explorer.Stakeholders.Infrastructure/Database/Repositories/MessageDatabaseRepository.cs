using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class MessageDatabaseRepository : IMessageRepository
    {
        private readonly StakeholdersContext _dbContext;
        private readonly DbSet<Message> _dbSet;
        public MessageDatabaseRepository(StakeholdersContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<Message>();
        }

        public PagedResult<Message> GetMessagesPagedById(int page, int pageSize, long messageId)
        {
            var task = _dbSet.Where(x => x.Id == messageId).GetPagedById(page, pageSize);
            task.Wait();
            return task.Result;
        }
    }
}
