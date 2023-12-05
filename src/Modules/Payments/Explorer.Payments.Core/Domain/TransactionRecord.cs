using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain
{
    public class TransactionRecord : Entity
    {
        public long Id { get; set; }
        public long RecieverId { get; set; }
        public long PayerId { get; set; }
        public long Amount { get; set; }
        public DateTime Date { get; set; }

        public TransactionRecord(long recieverId, long payerId, long amount)
        {
            RecieverId = recieverId;
            PayerId = payerId;
            Amount = amount;
            Date = DateTime.UtcNow;
        }
    }
}
