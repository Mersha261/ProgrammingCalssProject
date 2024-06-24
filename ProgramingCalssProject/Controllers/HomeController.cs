using Microsoft.AspNetCore.Mvc;
using PgrogrammingClass.Core.Domain;
using PgrogrammingClass.Data.DataContext;
using PgrogrammingClass.Sevices.EntitesServices;
using ProgramingCalssProject.Models;
using System.Diagnostics;

namespace ProgramingCalssProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAboutUsService _aboutUsService;
        public HomeController(ILogger<HomeController> logger, IAboutUsService aboutUsService)
        {
            _logger = logger;
            _aboutUsService = aboutUsService;
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