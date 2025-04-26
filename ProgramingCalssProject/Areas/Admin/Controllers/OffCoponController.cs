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
    public class OffCoponController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OffCoponController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/OffCopon
        public async Task<IActionResult> Index()
        {
              return _context.TblOffCopon != null ? 
                          View(await _context.TblOffCopon.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.TblOffCopon'  is null.");
        }

        // GET: Admin/OffCopon/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TblOffCopon == null)
            {
                return NotFound();
            }

            var tblOffCopon = await _context.TblOffCopon
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblOffCopon == null)
            {
                return NotFound();
            }

            return View(tblOffCopon);
        }

        // GET: Admin/OffCopon/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/OffCopon/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CoponName,Percent,Id,CreateDate,ModifyDate")] TblOffCopon tblOffCopon)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblOffCopon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblOffCopon);
        }

        // GET: Admin/OffCopon/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TblOffCopon == null)
            {
                return NotFound();
            }

            var tblOffCopon = await _context.TblOffCopon.FindAsync(id);
            if (tblOffCopon == null)
            {
                return NotFound();
            }
            return View(tblOffCopon);
        }

        // POST: Admin/OffCopon/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CoponName,Percent,Id,CreateDate,ModifyDate")] TblOffCopon tblOffCopon)
        {
            if (id != tblOffCopon.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblOffCopon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblOffCoponExists(tblOffCopon.Id))
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
            return View(tblOffCopon);
        }

        // GET: Admin/OffCopon/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TblOffCopon == null)
            {
                return NotFound();
            }

            var tblOffCopon = await _context.TblOffCopon
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblOffCopon == null)
            {
                return NotFound();
            }

            return View(tblOffCopon);
        }

        // POST: Admin/OffCopon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TblOffCopon == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TblOffCopon'  is null.");
            }
            var tblOffCopon = await _context.TblOffCopon.FindAsync(id);
            if (tblOffCopon != null)
            {
                _context.TblOffCopon.Remove(tblOffCopon);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblOffCoponExists(int id)
        {
          return (_context.TblOffCopon?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
