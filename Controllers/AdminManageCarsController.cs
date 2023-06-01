using CarTrader.Data;
using CarTrader.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarTrader.Controllers
{
    public class AdminManageCarsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly UserManager<User> _userManager;

        public AdminManageCarsController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment, UserManager<User> userManager)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _userManager = userManager;
        }


        // GET: AdminManageCars/
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var applicationDbContext = _context.Car.Include(c => c.User);
            return View(await applicationDbContext.ToListAsync());
        }


        // GET: ManageCars/Edit/5
        [Authorize]
        public async Task<IActionResult> Cancel(int? id)
        {
            if (id == null || _context.Car == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var car = await _context.Car.FirstOrDefaultAsync(c => c.Id == id && c.UserId == user.Id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }
        // POST: ManageCars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(int id, [Bind("Id,Make,Model,Price,Sold")] Car car)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (id != car.Id || car.UserId != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        private bool CarExists(int? id)
        {
            throw new NotImplementedException();
        }

        private bool CarExists(int id)
        {
            return (_context.Car?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
