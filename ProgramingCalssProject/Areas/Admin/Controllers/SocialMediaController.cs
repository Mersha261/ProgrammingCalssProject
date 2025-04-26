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
    public class SocialMediaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SocialMediaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/SocialMedia
        public async Task<IActionResult> Index()
        {
              return _context.TblSocialMedia != null ? 
                          View(await _context.TblSocialMedia.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.TblSocialMedia'  is null.");
        }

        // GET: Admin/SocialMedia/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TblSocialMedia == null)
            {
                return NotFound();
            }

            var tblSocialMedia = await _context.TblSocialMedia
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblSocialMedia == null)
            {
                return NotFound();
            }

            return View(tblSocialMedia);
        }

        // GET: Admin/SocialMedia/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/SocialMedia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Link,Icon,Id,CreateDate,ModifyDate")] TblSocialMedia tblSocialMedia)
        {
            if (ModelState.IsValid)
            {
                tblSocialMedia.CreateDate = DateTime.Now;
                tblSocialMedia.ModifyDate = DateTime.Now;
                _context.Add(tblSocialMedia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblSocialMedia);
        }

        // GET: Admin/SocialMedia/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TblSocialMedia == null)
            {
                return NotFound();
            }

            var tblSocialMedia = await _context.TblSocialMedia.FindAsync(id);
            if (tblSocialMedia == null)
            {
                return NotFound();
            }
            return View(tblSocialMedia);
        }

        // POST: Admin/SocialMedia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Link,Icon,Id,CreateDate,ModifyDate")] TblSocialMedia tblSocialMedia)
        {
            if (id != tblSocialMedia.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    tblSocialMedia.ModifyDate = DateTime.Now;
                    _context.Update(tblSocialMedia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblSocialMediaExists(tblSocialMedia.Id))
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
            return View(tblSocialMedia);
        }

        // GET: Admin/SocialMedia/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TblSocialMedia == null)
            {
                return NotFound();
            }

            var tblSocialMedia = await _context.TblSocialMedia
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblSocialMedia == null)
            {
                return NotFound();
            }

            return View(tblSocialMedia);
        }

        // POST: Admin/SocialMedia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TblSocialMedia == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TblSocialMedia'  is null.");
            }
            var tblSocialMedia = await _context.TblSocialMedia.FindAsync(id);
            if (tblSocialMedia != null)
            {
                _context.TblSocialMedia.Remove(tblSocialMedia);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblSocialMediaExists(int id)
        {
          return (_context.TblSocialMedia?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
