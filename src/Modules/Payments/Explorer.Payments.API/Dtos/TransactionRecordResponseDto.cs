using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Dtos
{
    public class TransactionRecordResponseDto
    {
        public long Id { get; set; }
        public long RecieverId { get; set; }
        public long PayerId { get; set; }
        public long Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
