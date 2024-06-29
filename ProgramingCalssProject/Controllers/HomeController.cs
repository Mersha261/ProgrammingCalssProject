using DNTCaptcha.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PgrogrammingClass.Core.Domain;
using PgrogrammingClass.Data.DataContext;
using PgrogrammingClass.Sevices.EntitesServices;
using ProgramingCalssProject.Models;
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
        public HomeController(
            ILogger<HomeController> logger,
            IAboutUsService aboutUsService,
            IContactUsService contactUsService,
            IDNTCaptchaValidatorService dNTCaptchaValidatorService,
            DNTCaptchaOptions dNTCaptchaOptions)
        {
            _logger = logger;
            _aboutUsService = aboutUsService;
            _contactUsService = contactUsService;
            _dNTCaptchaValidatorService = dNTCaptchaValidatorService;
            _dNTCaptchaOptions = dNTCaptchaOptions;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            return View(_aboutUsService.GetAll().Result.FirstOrDefault());
        }

        public IActionResult ContactUs()
        {
            return View(_aboutUsService.GetAll().Result.FirstOrDefault());
        }

        [HttpPost]
        public IActionResult ContactUsForm(TblContactUs model, string DNT_CaptchaInputText)
        {
            if (!ModelState.IsValid)
            {
               var test= Request;
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