using AutoMapper;
using SwapApp.Entities;
using SwapApp.Models;

namespace SwapApp.Services
{
    public interface IItemService
    {
        int Create(AddItemDto addItem);
        IEnumerable<ItemDto> GetAll();
        ItemDto GetById(int id);
    }

    public class ItemService : IItemService
    {
        private readonly ItemDbContext _dbContext;
        private readonly IMapper _mapper;

        public ItemService(ItemDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public ItemDto GetById(int id)
        {
            var item = _dbContext.Item.FirstOrDefault(x => x.Id == id);
            if (item is null) return null;
            var result = _mapper.Map<ItemDto>(item);
            return result;
        }

        public IEnumerable<ItemDto> GetAll()
        {
            var items = _dbContext.Item.ToList();
            var itemsDtos = _mapper.Map<List<ItemDto>>(items);
            return itemsDtos;

        }

        public int Create(AddItemDto addItem)
        {
            var item = _mapper.Map<Item>(addItem);
            _dbContext.Item.Add(item);
            _dbContext.SaveChanges();
            return item.Id;
        }
    }
}
