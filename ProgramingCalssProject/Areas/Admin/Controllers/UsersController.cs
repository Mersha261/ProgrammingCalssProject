using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PgrogrammingClass.Core.Domain;
using PgrogrammingClass.Data.DataContext;
using ProgramingCalssProject.Models;
using ProgramingCalssProject.Models.Utillity;

namespace ProgramingCalssProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class UsersController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly IpersianDateTime _dt;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UsersController(
            ApplicationDbContext context,
            IpersianDateTime dt,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _dt = dt;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {

            return View(_context.TblUser.ToList());
        }

        public async Task<IActionResult> Details(string id)
        {
            var user = _context.TblUser.Find(id);
            return View(user);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var thisUser = _context.TblUser.Find(id);
            var currentUser =await _userManager.GetUserAsync(User);
            if(thisUser.Id==currentUser.Id)
            {
                TempData["W"] = "شما نمیتوانید حساب کاربری خودتان را حذف کنید";
                return RedirectToAction(nameof(Details), new { id = id });
            }
            var checkShoppingcart = _context.TblShoppingcart.Where(a => a.UserId == id).ToList();
            if (checkShoppingcart.Count() > 0)
            {
                TempData["W"] = "این کاربر دارای سبد خرید است و شما امکان حذف این کاربر را ندارید";
                return RedirectToAction(nameof(Details), new { id = id });
            }
            await _userManager.UpdateSecurityStampAsync(thisUser);
            _context.Remove(User);
            TempData["W"] = ErrMsg.Deleted;
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> ActiveAdmin(string id)
        {
            var thisUser = _context.TblUser.Find(id);
            var currentUser = await _userManager.GetUserAsync(User);
            if (thisUser.Id == currentUser.Id)
            {
                TempData["W"] = "شما نمیتوانید سطح دسترسی خودتان را تغییر دهید";
                return RedirectToAction(nameof(Details), new { id = id });
            }
            await _userManager.AddToRoleAsync(thisUser, "Admin");

            await _userManager.UpdateSecurityStampAsync(thisUser);

            TempData["S"] = ErrMsg.Edited;
            return RedirectToAction(nameof(Index));

        }


        public async Task<IActionResult> DeActiveAdmin(string id)
        {
            var thisUser = _context.TblUser.Find(id);
            var currentUser = await _userManager.GetUserAsync(User);
            if (thisUser.Id == currentUser.Id)
            {
                TempData["W"] = "شما نمیتوانید سطح دسترسی خودتان را تغییر دهید";
                return RedirectToAction(nameof(Details), new { id = id });
            }
            await _userManager.RemoveFromRoleAsync(thisUser, "Admin");

            await _userManager.UpdateSecurityStampAsync(thisUser);

            TempData["S"] = ErrMsg.Edited;
            return RedirectToAction(nameof(Index));

        }

    }
}
