using AutoMapper;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.Bundles;
using Explorer.Payments.Core.Domain.ShoppingCarts;

namespace Explorer.Payments.Core.Mappers;

public class PaymentsProfile : Profile
{
    public PaymentsProfile()
    {
        CreateMap<ShoppingCartResponseDto, ShoppingCart>().ReverseMap().ForMember(x => x.OrderItems, opt => opt.MapFrom(src => src.OrderItems));
        CreateMap<ShoppingCartCreateDto, ShoppingCart>().ReverseMap();
        CreateMap<ShoppingCartUpdateDto, ShoppingCart>().ReverseMap();

        CreateMap<OrderItemResponseDto, OrderItem>().ReverseMap();
        CreateMap<OrderItemCreateDto, OrderItem>().ReverseMap();
        CreateMap<OrderItemUpdateDto, OrderItem>().ReverseMap();

        CreateMap<TourTokenResponseDto, TourToken>().ReverseMap();
        CreateMap<TourTokenCreateDto, TourToken>().ReverseMap();

        CreateMap<TourSale, TourSaleResponseDto>();
        CreateMap<TourSaleCreateDto, TourSale>();
        CreateMap<TourSaleUpdateDto, TourSale>();

        CreateMap<WalletResponseDto, Wallet>().ReverseMap();
        CreateMap<WalletUpdateDto, Wallet>().ReverseMap();
        CreateMap<WalletCreateDto, Wallet>().ReverseMap();

        CreateMap<RecordCreateDto, Record>().ReverseMap();
        CreateMap<RecordResponseDto, Record>().ReverseMap();

        CreateMap<TransactionRecordCreateDto, TransactionRecord>().ReverseMap();
        CreateMap<TransactionRecordResponseDto, TransactionRecord>().ReverseMap();

        CreateMap<ShoppingNotificationCreateDto, ShoppingNotification>().ReverseMap();
        CreateMap<ShoppingNotificationResponseDto, ShoppingNotification>().ReverseMap();

        CreateMap<CouponResponseDto, Coupon>().ReverseMap();
        CreateMap<CouponCreateDto, Coupon>().ReverseMap();
        CreateMap<CouponUpdateDto, Coupon>().ReverseMap();

        CreateMap<BundleOrderItem, BundleOrderItemResponseDto>().ReverseMap();
        CreateMap<BundleItem, BundleItemResponseDto>().ReverseMap();
        CreateMap<BundleResponseDto, Bundle>().ReverseMap().ForMember(x => x.BundleItems, opt => opt.MapFrom(src => src.BundleItems));
        CreateMap<BundleRecordResponseDto, BundleRecord>().ReverseMap();

        CreateMap<WishlistResponseDto, Wishlist>().ReverseMap();
        CreateMap<WishlistCreateDto, Wishlist>().ReverseMap();
        CreateMap<WishlistUpdateDto, Wishlist>().ReverseMap();
    }
}
