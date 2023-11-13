using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain.ShoppingCarts
{
    public class OrderItem : Entity
    {
        public long TourId { get; init; }
        public string TourName { get; init; }
        public double Price { get; init; }
        public long ShoppingCartId { get; init; }

        public OrderItem(long tourId, string tourName, double price, long shoppingCartId) 
        {
            TourId = tourId;
            TourName = tourName;
            Price = price;
            ShoppingCartId = shoppingCartId;
        }
    }
}
