using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Dtos
{
    public class WalletUpdateDto
    {
        public long Id { get; set; }
        public long AdventureCoin { get; set; }

        public WalletUpdateDto(long id,long adventureCoin) {
            Id = id;
            AdventureCoin = adventureCoin;
        }
    }
}
