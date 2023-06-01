using Microsoft.AspNetCore.Identity;

namespace CarTrader.Models
{
    public class User : IdentityUser
    {
        public bool IsBlocked { get; set; } = false;

    }
}
