using System.ComponentModel.DataAnnotations;

namespace SwapApp.Models
{
    public class RegisterUserDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
        [Required]
        [MinLength(3)]
        public string Name { get; set; }
        public int RoleId { get; set; } = 1;

    }
}
