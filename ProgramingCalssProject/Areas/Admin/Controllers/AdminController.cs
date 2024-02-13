using Microsoft.AspNetCore.Mvc;
using PgrogrammingClass.Sevices.EntitesServices;

namespace ProgramingCalssProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        private readonly IAboutUsService _aboutUsService;

        public AdminController(IAboutUsService aboutUsService)
        {
            _aboutUsService= aboutUsService;
        }



        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AboutUs()
        {
            return View(_aboutUsService.GetAll().Result.FirstOrDefault());
        }
    }
}
