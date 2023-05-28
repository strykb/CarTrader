using CarTrader.Data;
using CarTrader.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarTrader.Controllers
{
    public class ManageCarsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly UserManager<IdentityUser> _userManager;

        public ManageCarsController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _userManager = userManager;
        }

        // GET: ManageCars/
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var applicationDbContext = _context.Car.Where(c => c.UserId == user.Id).Include(c => c.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ManageCars/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: ManageCars/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Make,Model,Price, Year, ImageFile")] CarDTO carDTO)
        {
            // get user
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            // get image name and extenstion
            string wwwRootPath = _hostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(carDTO.ImageFile.FileName);
            string extension = Path.GetExtension(carDTO.ImageFile.FileName);
            fileName = fileName + Guid.NewGuid().ToString() + extension;
            var car = new Car
            {
                Id = carDTO.Id,
                PublishedAt = DateTime.Now,
                UserId = user.Id,
                Make = carDTO.Make,
                Model = carDTO.Model,
                Price = carDTO.Price,
                Image = fileName
            };
            string[] acceptedFileExtensions = { ".jpg", ".png", ".jpeg" };
            if (ModelState.IsValid && acceptedFileExtensions.Contains(extension.ToLower()))
            {
                // save image
                string path = Path.Combine(wwwRootPath + "/img/", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await carDTO.ImageFile.CopyToAsync(fileStream);
                }
                _context.Add(car);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(carDTO);
        }

        // GET: ManageCars/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Make,Model,Price,Sold")] Car car)
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

        // GET: ManageCars/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (id == null || _context.Car == null)
            {
                return NotFound();
            }

            var car = await _context.Car.FirstOrDefaultAsync(c => c.Id == id && c.UserId == user.Id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: ManageCars/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Car == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Car'  is null.");
            }
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var car = await _context.Car.FirstOrDefaultAsync(c => c.Id == id && c.UserId == user.Id);
            if (car != null)
            {
                _context.Car.Remove(car);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(int id)
        {
            return (_context.Car?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}
