using Microsoft.AspNetCore.Authorization;
using SwapApp.Entities;
using SwapApp.Exceptions;

namespace SwapApp.Services
{
    public interface IItemReservationService
    {
        bool AddReservation(int id);
    }

    public class ItemReservationService : IItemReservationService
    {
        private readonly ItemDbContext _dbContext;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _contextService;

        public ItemReservationService(ItemDbContext dbContext, IAuthorizationService authorizationService, IUserContextService contextService)
        {
            _dbContext = dbContext;
            _authorizationService = authorizationService;
            _contextService = contextService;
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
