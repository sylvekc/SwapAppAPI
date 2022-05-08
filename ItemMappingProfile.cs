using AutoMapper;
using SwapApp.Entities;
using SwapApp.Models;

namespace SwapApp
{
    public class ItemMappingProfile : Profile
    {
        public ItemMappingProfile()
        {
            CreateMap<Item, ItemDto>();
            CreateMap<AddItemDto, Item>();
        }
    }
}
