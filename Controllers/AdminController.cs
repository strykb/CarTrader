using CarTrader.Data;
using CarTrader.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace CarTrader.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<User> _userManager;

        public AdminController(ApplicationDbContext dbContext, UserManager<User> userManager)
        {
            this.dbContext = dbContext;
            this._userManager = userManager;
        }

        [HttpGet]
        [Route("admin/userlist")]
        public IActionResult UserList()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        [HttpGet]
        [Route("admin/blocked-users")]
        public IActionResult BlockedUsers()
        {
            var blockedUsers = _userManager.Users.Where(u => u.IsBlocked).ToList();
            return View(blockedUsers);
        }

    }
}
