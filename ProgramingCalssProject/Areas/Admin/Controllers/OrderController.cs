using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PgrogrammingClass.Data.DataContext;
using ProgramingCalssProject.Models;

namespace ProgramingCalssProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.TblShoppingcart.Where(a => a.IsPaied).ToList());
        }

        public IActionResult Details(string Id)
        {
            return View(_context.TblShoppingcart
                .Where(a => a.Id == Id)
                .Include(a => a.TblShoppingCartDetails)
                .Include(a => a.TblUserAddress)
                .ThenInclude(a => a.TblCity)
                .ThenInclude(a => a.TblProvince).SingleOrDefault());
        }

        public async Task<IActionResult> Sent(string Id)
        {
            var model = _context.TblShoppingcart.Where(a => a.Id == Id).Single();
            model.IsSentToUser = true;
            _context.Update(model);
            await _context.SaveChangesAsync();
           
            TempData["S"] = ErrMsg.Edited;
            return RedirectToAction(nameof(Details), new { Id = Id });
        }
    }
}
