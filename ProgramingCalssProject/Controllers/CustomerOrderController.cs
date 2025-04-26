using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PgrogrammingClass.Core.Domain;
using PgrogrammingClass.Data.DataContext;
using ProgramingCalssProject.Models.Utillity;

namespace ProgramingCalssProject.Controllers
{

    [Authorize]
    public class CustomerOrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IpersianDateTime _dt;
        private readonly UserManager<ApplicationUser> _userManager;

        public CustomerOrderController(
            ApplicationDbContext context,
            IpersianDateTime dt,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _dt = dt;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var currentuser = await _userManager.GetUserAsync(User);

            var model = _context.TblShoppingcart.Where(a => a.UserId == currentuser.Id && a.IsPaied).ToList();


            return View(model);
        }


        public async Task<IActionResult> Details(string Id)
        {
            var currentuser = await _userManager.GetUserAsync(User);
            var model = _context.TblShoppingcart
                            .Where(a => a.Id == Id && a.UserId==currentuser.Id && a.IsPaied)
                            .Include(a => a.TblShoppingCartDetails)
                            .Include(a => a.TblUserAddress)
                            .ThenInclude(a => a.TblCity)
                            .ThenInclude(a => a.TblProvince).SingleOrDefault();

            return View(model);
        }


    }
}
