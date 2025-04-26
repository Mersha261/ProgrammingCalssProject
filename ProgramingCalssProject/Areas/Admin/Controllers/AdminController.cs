using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PgrogrammingClass.Core.Domain;
using PgrogrammingClass.Core.Utilitty;
using PgrogrammingClass.Data.DataContext;
using PgrogrammingClass.Sevices.EntitesServices;
using ProgramingCalssProject.Models;

namespace ProgramingCalssProject.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin")]
    [Area("Admin")]
    public class AdminController : Controller
    {
        private readonly IAboutUsService _aboutUsService;
        private readonly IContactUsService _contactUsService;
        private readonly ApplicationDbContext _context;
        public AdminController(IAboutUsService aboutUsService, IContactUsService contactUsService, ApplicationDbContext context)
        {
            _aboutUsService = aboutUsService;
            _contactUsService = contactUsService;
            _context = context;
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
            if (tblaboutUs != null)
            {
                tblaboutUs.AboutUs = model.AboutUs;
                tblaboutUs.IndexSentens = model.IndexSentens;
                tblaboutUs.ContactUs = model.ContactUs;
                tblaboutUs.AboutUsFooter = model.AboutUsFooter;
                tblaboutUs.Email = model.Email;
                tblaboutUs.PhoneNumbers = model.PhoneNumbers;
                tblaboutUs.Address = model.Address;
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

        public async Task<IActionResult> Shipping()
        {
            return View(await _context.TblPost.FirstOrDefaultAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Shipping(TblPost model)
        {
            if (model.CreateDate < DateTime.Now.AddYears(-100))
            {
                model.CreateDate= DateTime.Now;
            }

            model.ModifyDate = DateTime.Now;

            _context.Update(model);
            await _context.SaveChangesAsync();
            TempData["S"] = ErrMsg.Edited;
            return View(await _context.TblPost.FirstOrDefaultAsync());
        }


    }
}
