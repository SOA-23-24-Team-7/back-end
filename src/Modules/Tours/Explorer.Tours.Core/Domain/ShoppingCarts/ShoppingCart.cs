using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Tours.Core.Domain.Tours;

namespace Explorer.Tours.Core.Domain.ShoppingCarts
{
    public class ShoppingCart : Entity
    {
        public long TouristId { get; init; }
        public double TotalPrice { get; set; }
        public bool IsPurchased { get; init; }

        public ICollection<OrderItem> OrderItems { get; } = new List<OrderItem>();


        public ShoppingCart(long touristId, bool isPurchased)
        {
            TouristId = touristId;
            SetTotalPrice();
            IsPurchased = isPurchased;  //provjeriti
        }


        public void AddOrderItem(OrderItem newOrderItem)
        {
            OrderItems.Add(newOrderItem);
        }

        public void RemoveOrderItem(long id)
        {
            var item = OrderItems.FirstOrDefault(x => x.Id== id);
            OrderItems.Remove(item);

        }


        public void SetTotalPrice()
        {
            TotalPrice = 0;
            if (OrderItems != null)
            {
                foreach (var items in OrderItems)
                {
                    TotalPrice += items.Price;
                }
            }
        }

    }
}
