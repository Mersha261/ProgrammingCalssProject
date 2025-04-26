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
    public class ContactUsFormController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContactUsFormController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/ContactUsForm
        public async Task<IActionResult> Index()
        {
              return _context.TblContactUs != null ? 
                          View(await _context.TblContactUs.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.TblContactUs'  is null.");
        }

        // GET: Admin/ContactUsForm/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TblContactUs == null)
            {
                return NotFound();
            }

            var tblContactUs = await _context.TblContactUs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblContactUs == null)
            {
                return NotFound();
            }
            tblContactUs.IsRead=true;
            tblContactUs.ModifyDate = DateTime.Now;
            _context.Update(tblContactUs);
            _context.SaveChanges(); 
            return View(tblContactUs);
        }

      
        // GET: Admin/ContactUsForm/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TblContactUs == null)
            {
                return NotFound();
            }

            var tblContactUs = await _context.TblContactUs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblContactUs == null)
            {
                return NotFound();
            }

            return View(tblContactUs);
        }

        // POST: Admin/ContactUsForm/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TblContactUs == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TblContactUs'  is null.");
            }
            var tblContactUs = await _context.TblContactUs.FindAsync(id);
            if (tblContactUs != null)
            {
                _context.TblContactUs.Remove(tblContactUs);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblContactUsExists(int id)
        {
          return (_context.TblContactUs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
