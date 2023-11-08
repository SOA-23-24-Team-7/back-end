using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class OrderItemUpdateDto
    {
        public long Id { get; set; }
        public double Price { get; set; }
    }

}
