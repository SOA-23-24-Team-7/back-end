using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class OrderItemResponseDto
    {
        public long Id { get; set; }
        public long TourId { get; set; }
        public string TourName { get; set; }
        public double Price { get; set; }
        public long ShoppingCartId { get; set; }
    }
}
