using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Tours.Core.Domain.Tours;

namespace Explorer.Tours.Core.Domain.ShoppingCarts
{
    public class ShoppingCart : Entity
    {
        public long TouristId { get; init; }
        public double TotalPrice { get; init; }
        public bool IsPurchased { get; init; }

        public ICollection<OrderItem> OrderItems { get; } = new List<OrderItem>();


        public ShoppingCart(long touristId, double totalPrice, bool isPurchased)
        {
            TouristId = touristId;
            TotalPrice = totalPrice;
            IsPurchased = isPurchased;  //provjeriti
        }


       /* public void AddOrderItem(long tourId, double price, string tourName, long shoppingCartId)
        {
            OrderItem storedItem = OrderItems.FirstOrDefault(r => r.TourId == tourId);
            if (storedItem != null)
            {
                throw new ArgumentException("Order item already exists.\n");
            }
            else
            {
                OrderItems.Add(new OrderItem(tourId, tourName, price, shoppingCartId));
            }
            

        }*/

    }
}
