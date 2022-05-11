using Microsoft.AspNetCore.Identity;
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
        private readonly IPasswordHasher<User> _passwordHasher;
        public AccountService(ItemDbContext itemDbContext, IPasswordHasher<User> passwordHasher)
        {
            _itemDbContext = itemDbContext;
            _passwordHasher = passwordHasher;
        }
        public void RegisterUser (RegisterUserDto registerDto)
        {
            var newUser = new User()
            {
                Email = registerDto.Email,
                Name = registerDto.Name,
                RoleId = registerDto.RoleId,
            };
            var hashedPassword = _passwordHasher.HashPassword(newUser, registerDto.Password);
            newUser.PasswordHash = hashedPassword;
            _itemDbContext.Users.Add(newUser);
            _itemDbContext.SaveChanges();
        }
    }
}
