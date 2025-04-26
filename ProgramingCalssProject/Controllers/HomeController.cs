using DNTCaptcha.Core;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Manage.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PgrogrammingClass.Core.Domain;
using PgrogrammingClass.Data.DataContext;
using PgrogrammingClass.Sevices.EntitesServices;
using ProgramingCalssProject.Models;
using ProgramingCalssProject.Models.ViewModel;
using System.Diagnostics;
using System.Reflection;

namespace ProgramingCalssProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAboutUsService _aboutUsService;
        private readonly IContactUsService _contactUsService;
        private readonly IDNTCaptchaValidatorService _dNTCaptchaValidatorService;
        private readonly DNTCaptchaOptions _dNTCaptchaOptions;
        private readonly ApplicationDbContext _context;
        public HomeController(
            ILogger<HomeController> logger,
            IAboutUsService aboutUsService,
            IContactUsService contactUsService,
            IDNTCaptchaValidatorService dNTCaptchaValidatorService,
            DNTCaptchaOptions dNTCaptchaOptions,
            ApplicationDbContext context)
        {
            _logger = logger;
            _aboutUsService = aboutUsService;
            _contactUsService = contactUsService;
            _dNTCaptchaValidatorService = dNTCaptchaValidatorService;
            _dNTCaptchaOptions = dNTCaptchaOptions;
            _context = context;
        }
        #region Other
        public IActionResult Index()
        {

            ViewBag.indexSentens = _context.TblAboutUs.FirstOrDefault().IndexSentens;
            var model = _context.Tblproduct.Where(a => !a.IsDeleted).Include(a => a.ProductImages).OrderBy(a => a.CreateDate).Take(10).ToList();
            model.AddRange(_context.Tblproduct.Where(a => !a.IsDeleted).Include(a => a.ProductImages).OrderBy(a => a.SoldCount).Take(10).ToList());
            model.AddRange(_context.Tblproduct.Where(a => !a.IsDeleted).Include(a => a.ProductImages).OrderBy(a => a.ViewCount).Take(10).ToList());
            return View(model);
        }

        public IActionResult AboutUs()
        {
            return View(_aboutUsService.GetAll().Result.FirstOrDefault());
        }

        public IActionResult ContactUs()
        {
            return View(_aboutUsService.GetAll().Result.FirstOrDefault());
        }
        public IActionResult ContactUsForm(TblContactUs model, string DNT_CaptchaInputText)
        {
            if (!ModelState.IsValid)
            {
                var test = Request;
                TempData["W"] = ErrMsg.ComplateInfo;
                return RedirectToAction(nameof(Index));
            }
            if (!_dNTCaptchaValidatorService.HasRequestValidCaptchaEntry())
            {
                TempData["W"] = "کد امنیتی وارد شده صحیح نیست";
                return RedirectToAction(nameof(Index));
            }

            model.CreateDate = DateTime.Now;
            model.ModifyDate = DateTime.Now;
            model.IsRead = false;
            model.UserIp = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            _contactUsService.Add(model);

            TempData["S"] = "پیغام شما با موفقیت برای ادمین ارسال شد";
            return RedirectToAction(nameof(Index));

        }

        #endregion

        #region Product

        public IActionResult Product(string? Title, int CatId = 0)
        {

            var model = _context.Tblproduct.Where(a => !a.IsDeleted)
              .Include(a => a.ProductImages)
              .Include(a => a.TblProductComments)
              .ToList();
            if (Title != null)
            {
                model = model.Where(a => a.Title.Contains(Title)).ToList();
            }
            if(CatId>0)
            {
                model=model.Where(a=>a.CategoryId==CatId).ToList();
            }

            return View(model);
        }

        public IActionResult ProductDetails(int id, string title)
        {
            return View(
                _context.Tblproduct.Where(a => !a.IsDeleted && a.Id == id)
                .Include(a => a.ProductImages)
                .Include(a => a.TblProductComments)
                .SingleOrDefault());
        }
        [HttpPost]

        #endregion

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

    }
}