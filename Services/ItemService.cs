using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using SwapApp.Authorization;
using SwapApp.Entities;
using SwapApp.Exceptions;
using SwapApp.Models;
using System.Security.Claims;

namespace SwapApp.Services
{
    public interface IItemService
    {
        int AddItem(AddItemDto addItem);
        PagedResult<GetItemDto> GetAllItems(ItemQuery query);
        GetItemDto GetItemById(int id);
        bool DeleteItem (int id);
        bool UpdateItem(UpdateItemDto updateItem, int id);

    }

    public class ItemService : IItemService
    {
        private readonly ItemDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<ItemService> _logger;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;

        public ItemService(ItemDbContext dbContext, IMapper mapper, ILogger<ItemService> logger, IAuthorizationService authorizationService, IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
        }

        public bool UpdateItem (UpdateItemDto updateItem, int id)
        {
            var item = _dbContext.Item.FirstOrDefault(x => x.Id == id);
            if (item is null)
                throw new NotFoundException("Item not found");

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, item, new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if(!authorizationResult.Succeeded)
            {
                throw new ForbidException("You can't update this item.");
            }

            item.Name = updateItem.Name;
            item.Description = updateItem.Description;
            item.District = updateItem.District;
            item.Street = updateItem.Street;
            item.City = updateItem.City;
            item.SwapFor = updateItem.SwapFor;
            item.ForFree = updateItem.ForFree;

            _dbContext.SaveChanges();
            return true;
        }

        public bool DeleteItem (int id)
        {
            _logger.LogError($"Item with id: {id} DELETE action invoked");
            var item = _dbContext.Item.FirstOrDefault(x => x.Id == id);
            if (item is null) return false;

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, item, new ResourceOperationRequirement(ResourceOperation.Delete)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException("You can't delete this item");
            }

            _dbContext.Item.Remove(item);
            _dbContext.SaveChanges();
            return true;
        }

        public GetItemDto GetItemById(int id)
        {
            var item = _dbContext.Item.FirstOrDefault(x => x.Id == id);
            if (item is null)
                throw new NotFoundException($"Item with id: {id} not found");
            var result = _mapper.Map<GetItemDto>(item);
            return result;
        }

        public PagedResult<GetItemDto> GetAllItems(ItemQuery query)
        {
            var baseQuery = _dbContext.Item
                .Where(d => DateTime.Compare(d.ExpiresAt, DateTime.Now) > 0 && d.IsPublic == true)
                .Where(e => query.Search == null || (e.Name.ToLower().Contains(query.Search.ToLower()) ||
                                                     e.Description.ToLower().Contains(query.Search.ToLower())))
                .Where(c => query.City == null || c.City.ToLower().Contains(query.City.ToLower()));

            var items = baseQuery.Skip(query.PageSize*query.PageNumber - query.PageSize)
                .Take(query.PageSize)
                .ToList();

            var totalItemsCount = items.Count();

            var itemsDtos = _mapper.Map<List<GetItemDto>>(items);

            var result = new PagedResult<GetItemDto>(itemsDtos, totalItemsCount, query.PageSize, query.PageNumber);
            return result;

        }

        public int AddItem(AddItemDto addItem)
        {
            var item = _mapper.Map<Item>(addItem);
            item.UserId = (int)_userContextService.GetUserId;
            item.CreatedAt = DateTime.Now;
            item.ExpiresAt = item.CreatedAt.AddDays(14);
            _dbContext.Item.Add(item);
            _dbContext.SaveChanges();
            return item.Id;
        }
    }
}
