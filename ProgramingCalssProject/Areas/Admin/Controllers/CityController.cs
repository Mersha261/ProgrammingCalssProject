using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PgrogrammingClass.Core.Domain;
using PgrogrammingClass.Data.DataContext;

namespace ProgramingCalssProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CityController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CityController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/City
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TblCity.Include(t => t.TblProvince);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/City/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TblCity == null)
            {
                return NotFound();
            }

            var tblCity = await _context.TblCity
                .Include(t => t.TblProvince)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblCity == null)
            {
                return NotFound();
            }

            return View(tblCity);
        }

        // GET: Admin/City/Create
        public IActionResult Create()
        {
            ViewData["ProvinceId"] = new SelectList(_context.TblProvince, "Id", "ProvinceName");
            return View();
        }

        // POST: Admin/City/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CityName,ProvinceId,Id")] TblCity tblCity)
        {
            ModelState.Remove(nameof(TblCity.TblProvince));
            ModelState.Remove(nameof(TblCity.TblUserAddresses));
            if (ModelState.IsValid)
            {
                tblCity.CreateDate= DateTime.Now;
                tblCity.ModifyDate= DateTime.Now;
                _context.Add(tblCity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProvinceId"] = new SelectList(_context.TblProvince, "Id", "ProvinceName", tblCity.ProvinceId);
            return View(tblCity);
        }

        // GET: Admin/City/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TblCity == null)
            {
                return NotFound();
            }

            var tblCity = await _context.TblCity.FindAsync(id);
            if (tblCity == null)
            {
                return NotFound();
            }
            ViewData["ProvinceId"] = new SelectList(_context.TblProvince, "Id", "ProvinceName", tblCity.ProvinceId);
            return View(tblCity);
        }

        // POST: Admin/City/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CityName,ProvinceId,Id,CreateDate,ModifyDate")] TblCity tblCity)
        {
            if (id != tblCity.Id)
            {
                return NotFound();
            }

            ModelState.Remove(nameof(TblCity.TblProvince));
            ModelState.Remove(nameof(TblCity.TblUserAddresses));

            if (ModelState.IsValid)
            {
                try
                {
                    tblCity.ModifyDate=DateTime.Now;
                    _context.Update(tblCity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblCityExists(tblCity.Id))
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
            ViewData["ProvinceId"] = new SelectList(_context.TblProvince, "Id", "ProvinceName", tblCity.ProvinceId);
            return View(tblCity);
        }

        // GET: Admin/City/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TblCity == null)
            {
                return NotFound();
            }

            var tblCity = await _context.TblCity
                .Include(t => t.TblProvince)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblCity == null)
            {
                return NotFound();
            }

            return View(tblCity);
        }

        // POST: Admin/City/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TblCity == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TblCity'  is null.");
            }
            var tblCity = await _context.TblCity.FindAsync(id);

            if(_context.TblUserAddress.Where(a=>a.CityId==tblCity.Id).ToList().Count()>0)
            {
                TempData["W"] = "برای این شهر حداقل یک آدرس ثبت شده است و شما قادر به حذف این شهر نمی باشید!!!";
                return RedirectToAction(nameof(Index));
            }

            if (tblCity != null)
            {
                _context.TblCity.Remove(tblCity);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblCityExists(int id)
        {
          return (_context.TblCity?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
