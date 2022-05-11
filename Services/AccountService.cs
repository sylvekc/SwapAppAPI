using SwapApp.Entities;
using SwapApp.Models;

namespace SwapApp.Services
{
    public interface IAccountService
    {
        void RegisterUser(RegisterUserDto registerDto);
    }
    public class AccountService : IAccountService
    {
        private readonly ItemDbContext _itemDbContext;
        public AccountService(ItemDbContext itemDbContext)
        {
            _itemDbContext = itemDbContext;
        }
        public void RegisterUser (RegisterUserDto registerDto)
        {
            var newUser = new User()
            {
                Email = registerDto.Email,
                Name = registerDto.Name,
                RoleId = registerDto.RoleId,
                PasswordHash = registerDto.Password
            };
            _itemDbContext.Users.Add(newUser);
            _itemDbContext.SaveChanges();
        }
    }
}
