using AutoMapper;
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
            CreateMap<ShoppingCartItem, CreateShoppingCartItemCommand>().ReverseMap();
        }
    }
}
