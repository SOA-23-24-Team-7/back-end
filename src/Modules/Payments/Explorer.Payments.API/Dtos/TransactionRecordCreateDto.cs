using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Dtos
{
    public class TransactionRecordCreateDto
    {
        public long RecieverId { get; set; }
        public long PayerId { get; set; }
        public long Amount { get; set; }

        public TransactionRecordCreateDto(long recieverId, long payerId, long amount)
        {
            RecieverId = recieverId;
            PayerId = payerId;
            Amount = amount;
        }
    }
}
