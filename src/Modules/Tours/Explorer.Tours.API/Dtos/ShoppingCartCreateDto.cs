using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class ShoppingCartCreateDto
    {
        public long TouristId { get; set; }
        public bool IsPurchased { get; set; }

    }
}
