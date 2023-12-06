using Explorer.BuildingBlocks.Core.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface IPublicKeyPointNotificationRepository
    {
        PagedResult<PublicKeyPointNotification> GetByAuthorId(int page, int pageSize, long authorId);
        int CountNotSeen(long userId);
        void Update(PublicKeyPointNotification notification);
    }
}
