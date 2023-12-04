using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Dtos
{
    public class WalletCreateDto
    {
        public long TouristId { get; set; }

        public WalletCreateDto(long touristId)
        {
            TouristId = touristId;
        }
    }
}
