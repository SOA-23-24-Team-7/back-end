using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain.ShoppingCarts;

public class BundleOrderItem : Entity
{
    public long BundleId { get; init; }
    public double Price { get; init; }
    public long ShoppingCartId { get; init; }

    public BundleOrderItem() { }

    public BundleOrderItem(long bundleId, double price, long shoppingCartId)
    {
        BundleId = bundleId;
        Price = price;
        ShoppingCartId = shoppingCartId;
    }
}
