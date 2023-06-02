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
        public IActionResult UserList()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        [HttpGet]
        public IActionResult BlockedUsers()
        {
            var blockedUsers = _userManager.Users.Where(u => u.IsBlocked).ToList();
            return View(blockedUsers);
        }

        [HttpPost]
        public IActionResult BlockUser(string userId)
        {
            var user = _userManager.FindByIdAsync(userId).Result;
            if (user == null)
            {
                return NotFound();
            }

            user.IsBlocked = true;
            var result = _userManager.UpdateAsync(user).Result;
            if (result.Succeeded)
            {
                return RedirectToAction("UserList");
            }
            else
            {
                ModelState.AddModelError("", "Wystąpił błąd podczas blokowania użytkownika.");
                return View("UserList");
            }
        }

        [HttpPost]
        public IActionResult UnblockUser(string userId)
        {
            var user = _userManager.FindByIdAsync(userId).Result;
            if (user != null)
            {
                user.IsBlocked = false;
                var result = _userManager.UpdateAsync(user).Result;
                if (result.Succeeded)
                {
                    return RedirectToAction("UserList");
                }
                else
                {
                    return RedirectToAction("BlockedUsers");
                }
            }
            else
            {
                return RedirectToAction("BlockedUsers");
            }
        }
    }
}
