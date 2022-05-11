using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SwapApp.Entities;
using SwapApp.Exceptions;
using SwapApp.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SwapApp.Services
{
    public interface IAccountService
    {
        void RegisterUser(RegisterUserDto registerDto);
        string GenerateJwt(LoginDto loginDto);
    }
    public class AccountService : IAccountService
    {
        private readonly ItemDbContext _itemDbContext;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;
        public AccountService(ItemDbContext itemDbContext, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings)
        {
            _itemDbContext = itemDbContext;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
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

        public string GenerateJwt(LoginDto loginDto)
        {
            var user = _itemDbContext.Users.Include(u=>u.Role).FirstOrDefault(e=>e.Email == loginDto.Email);
            if (user is null)
            {
                throw new BadRequestException("Nieprawidłowa nazwa użytkownaika lub hasło");
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginDto.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Nieprawidłowa nazwa użytkowanika lub hasło");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.Name}"),
                new Claim(ClaimTypes.Role, $"{user.Role.Name}"),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer, _authenticationSettings.JwtIssuer, claims,
                expires: expires,
                signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);

        }

    }
}
