using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using SwapApp.Entities;
using SwapApp.Exceptions;
using SwapApp.Models;

namespace SwapApp.Services
{
    public interface IItemReservationService
    {
        bool AddReservation(int id);
        PagedResult<GetItemDto> GetAllItemsReservedByUser(ItemQuery query);
    }

    public class ItemReservationService : IItemReservationService
    {
        private readonly ItemDbContext _dbContext;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _contextService;
        private readonly IMapper _mapper;

        public ItemReservationService(ItemDbContext dbContext, IAuthorizationService authorizationService, IUserContextService contextService, IMapper mapper)
        {
            _dbContext = dbContext;
            _authorizationService = authorizationService;
            _contextService = contextService;
            _mapper = mapper;
        }

        public PagedResult<GetItemDto> GetAllItemsReservedByUser(ItemQuery query)
        {
            var userId = _contextService.GetUserId;
            var baseQuery = _dbContext.Item
                .Where(c => c.ReservedBy == userId)
                .Where(d => DateTime.Compare(DateTime.Now, d.ExpiresAt) < 0 && d.IsPublic == true)
                .Where(e => query.Search == null || (e.Name.ToLower().Contains(query.Search.ToLower()) ||
                                                     e.Description.ToLower().Contains(query.Search.ToLower())));

            if (!string.IsNullOrEmpty(query.SortBy))
            {

                var columnsSelectors = new Dictionary<string, Expression<Func<Item, object>>>
                {
                    {nameof(Item.Name), r => r.Name},
                    {nameof(Item.CreatedAt), r => r.CreatedAt},
                };

                var selectedColumn = columnsSelectors[query.SortBy];

                baseQuery = query.SortDirection == SortDirection.ASC
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var userItems = baseQuery.Include(f => f.ItemPhotos).Skip(query.PageSize * query.PageNumber - query.PageSize)
                .Take(query.PageSize)
                .ToList();

            var totalItemsCount = userItems.Count();

            var itemsDtos = _mapper.Map<List<GetItemDto>>(userItems);

            var result = new PagedResult<GetItemDto>(itemsDtos, totalItemsCount, query.PageSize, query.PageNumber);
            return result;
        }


        public bool AddReservation(int id)
        {
            var item = _dbContext.Item.FirstOrDefault(x => x.Id == id);
            if (item.ReservedBy != null)
            {
                throw new ForbidException("This item is currently reserved");
            }

            item.ReservedBy = _contextService.GetUserId;
            _dbContext.SaveChanges();
            return true;
        }

    }
}
