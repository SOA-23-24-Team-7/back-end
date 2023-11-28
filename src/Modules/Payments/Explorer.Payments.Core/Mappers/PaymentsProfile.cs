using AutoMapper;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.Core.Domain;
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

        CreateMap<WalletResponseDto, Wallet>().ReverseMap();
        CreateMap<WalletUpdateDto, Wallet>().ReverseMap();
        CreateMap<WalletCreateDto, Wallet>().ReverseMap();
    }
}
