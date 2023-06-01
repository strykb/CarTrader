using CarTrader.Data;
using CarTrader.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarTrader.Controllers
{
    [Authorize(Roles = "Admin")]
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
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var applicationDbContext = _context.Car.Include(c => c.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // POST: ManageCars/Approve/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var car = await _context.Car.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            car.Approved = true;
            _context.Update(car);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: ManageCars/Cancel/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var car = await _context.Car.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            car.Approved = false;
            car.Canceled = true;
            _context.Update(car);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: ManageCars/Cancel/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reset(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var car = await _context.Car.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            car.Approved = false;
            car.Canceled = false;
            _context.Update(car);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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
