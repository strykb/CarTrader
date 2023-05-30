using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace CarTrader.Models
{
    public class User : IdentityUser
    {
        public new int Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 6)]
        [RegularExpression(@"^[A-Za-z0-9]+$")]
        public new string UserName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength (40)]
        public new string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]+$", ErrorMessage = "Password must contain at least one lowercase letter, one uppercase letter, one digit, and one special character.")]
        public new string PasswordHash { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Hasła nie są zgodne.")]
        public string ConfirmPassword { get; set; }

        public bool IsBlocked { get; set; }

        public User()
        {
            UserName = string.Empty;
            Email = string.Empty;
            PasswordHash = string.Empty;
            ConfirmPassword = string.Empty;
        }
    }
}
