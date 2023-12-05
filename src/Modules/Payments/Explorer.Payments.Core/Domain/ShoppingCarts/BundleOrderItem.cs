using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain.ShoppingCarts;

public class BundleOrderItem
{
    public long BundleId { get; init; }
    public string BundleName { get; init; }
    public double Price { get; init; }
    public long ShoppingCartId { get; init; }

    public BundleOrderItem(long bundleId, string tourName, double price, long shoppingCartId)
    {
        BundleId = bundleId;
        BundleName = tourName;
        Price = price;
        ShoppingCartId = shoppingCartId;
    }
}
