using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain
{
    public class Wallet : Entity
    {
        public long Id { get; set; }
        public long TouristId { get; set; }
        public long AdventureCoin { get; private set; } = 0;

        public void SetAdventureCoin(long adventureCoin)
        {
            if(adventureCoin> 0)
            {
                this.AdventureCoin = adventureCoin;
            }
        }
    }
}
