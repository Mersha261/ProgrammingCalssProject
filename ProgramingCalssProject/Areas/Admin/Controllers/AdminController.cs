using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PgrogrammingClass.Core.Domain;
using PgrogrammingClass.Core.Utilitty;
using PgrogrammingClass.Sevices.EntitesServices;
using ProgramingCalssProject.Models;

namespace ProgramingCalssProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        private readonly IAboutUsService _aboutUsService;
        private readonly IContactUsService _contactUsService;
        public AdminController(IAboutUsService aboutUsService, IContactUsService contactUsService)
        {
            _aboutUsService = aboutUsService;
            _contactUsService = contactUsService;
        }



        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AboutUs()
        {
            return View(_aboutUsService.GetAll().Result.FirstOrDefault());
        }

        [HttpPost]
        public async Task<IActionResult> AboutUs(TblAboutUs model)
        {
            var tblaboutUs = _aboutUsService.GetAll().Result.FirstOrDefault();
            if(tblaboutUs != null)
            {
                tblaboutUs.AboutUs = model.AboutUs;
                tblaboutUs.ContactUs = model.ContactUs;
                tblaboutUs.ModifyDate = DateTime.Now;
                _aboutUsService.Update(tblaboutUs);
                TempData["S"] = ErrMsg.Success;
                return View(tblaboutUs);
            }
            else
            {
                model.CreateDate = DateTime.Now;
                model.ModifyDate = DateTime.Now;

                _aboutUsService.Update(model);
                TempData["S"] = ErrMsg.Success;
                return View(tblaboutUs);
            }
        }

    }
}
