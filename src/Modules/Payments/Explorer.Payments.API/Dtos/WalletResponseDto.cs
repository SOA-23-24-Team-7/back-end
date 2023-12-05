using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Dtos
{
    public class WalletResponseDto
    {
        public long Id { get; set; }
        public long TouristId { get; set; }
        public long AdventureCoin { get; set; }
    }
}
