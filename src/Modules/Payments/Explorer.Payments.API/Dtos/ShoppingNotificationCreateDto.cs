using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Dtos
{
    public class ShoppingNotificationCreateDto
    {
        public long TourId { get; init; }
        public long TouristId { get; init; }
        public string Description { get; set; }
        public DateTime? Created { get; set; }
    }
}
