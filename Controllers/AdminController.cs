using CarTrader.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace CarTrader.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public AdminController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        [Route("admin/userlist")]
        [Authorize(Roles = "Admin")]
        public IActionResult UserList()
        {
            var users = dbContext.Users.ToList();
            return View(users);
        }

        [HttpGet]
        [Route("admin/blocked-users")]
        public IActionResult BlockedUsers()
        {
            var blockedUsers = dbContext.Users.Where(u => u.IsBlocked).ToList();
            return View(blockedUsers);
        }

    }
}
