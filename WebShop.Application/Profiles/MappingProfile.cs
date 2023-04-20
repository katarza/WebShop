using AutoMapper;
using WebShop.Application.Features.Orders.Commands;
using WebShop.Application.Features.Orders.Queries;
using WebShop.Application.Features.Products.Queries;
using WebShop.Application.Features.ShoppingCartItems.Commands.CreateShoppingCartItem;
using WebShop.Application.Features.ShoppingCartItems.Queries.GetShoppingCartItemsList;
using WebShop.Domain.Entities;

namespace WebShop.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ShoppingCartItem, ShoppingCardItemsListVM>().ReverseMap();
            CreateMap<Product, ProductDTO>();
            CreateMap<Product, ProductsListVM>();
            CreateMap<ShoppingCartItem, CreateShoppingCartItemCommand>().ReverseMap();
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<Order, OrdersListVM>().ReverseMap();
            CreateMap<Order, CreateOrderCommand>().ReverseMap();
            CreateMap<Address, ShippingAddressDTO>().ReverseMap();
            CreateMap<Address, ShippingAddressListDTO>().ReverseMap();


        }
    }
}
