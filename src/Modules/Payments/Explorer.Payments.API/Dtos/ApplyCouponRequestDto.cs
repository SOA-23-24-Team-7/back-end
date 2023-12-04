using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Dtos
{
    public class ApplyCouponRequestDto
    {
        public string CouponCode { get; set; }
        public long ShoppingCartId { get; set; }
    }
}
