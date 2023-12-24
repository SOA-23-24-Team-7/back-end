using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Public;
public interface ITourStatisticsService
{
    public int GetNumberOfBoughtToursForAuthor(long authorId);
    public int GetNumberOfTimesTheTourWasSold(long tourId);
}
