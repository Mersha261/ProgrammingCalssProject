using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PgrogrammingClass.Core.Domain;
using PgrogrammingClass.Data.DataContext;
using PgrogrammingClass.Sevices.EntitesServices;
using ProgramingCalssProject.Models;

namespace ProgramingCalssProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProvinceController : Controller
    {
        private readonly IProvinceService _provinceService;

        public ProvinceController(IProvinceService provinceService)
        {
            _provinceService = provinceService;
        }

        // GET: Admin/Province
        public async Task<IActionResult> Index()
        {
            return View(await _provinceService.GetAll());
        }

        // GET: Admin/Province/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0)
            {
                TempData["W"] = ErrMsg.NotFound;
                return RedirectToAction("Error", "Home");
            }

            var tblProvince = await _provinceService.GetById(id);

            if (tblProvince == null)
            {
                TempData["W"] = ErrMsg.NotFound;
                return RedirectToAction("Error", "Home");
            }

            return View(tblProvince);
        }

        // GET: Admin/Province/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Province/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProvinceName")] TblProvince tblProvince)
        {
            ModelState.Remove(nameof(tblProvince.TblCities));

            if (ModelState.IsValid)
            {
                tblProvince.CreateDate = DateTime.Now;
                tblProvince.ModifyDate = DateTime.Now;

                await _provinceService.Add(tblProvince);
                TempData["S"] = ErrMsg.Success;
                return RedirectToAction(nameof(Index));
            }
            TempData["W"] = ErrMsg.ComplateInfo;
            return View(tblProvince);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                TempData["W"] = ErrMsg.NotFound;
                return RedirectToAction("Error", "Home");
            }

            var tblProvince = await _provinceService.GetById(id);

            if (tblProvince == null)
            {
                TempData["W"] = ErrMsg.NotFound;
                return RedirectToAction("Error", "Home");
            }

            return View(tblProvince);
        }

        // POST: Admin/Province/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProvinceName,Id,CreateDate,ModifyDate")] TblProvince tblProvince)
        {
            ModelState.Remove(nameof(tblProvince.TblCities));
            if (id != tblProvince.Id)
            {
                TempData["W"] = ErrMsg.NotFound;
                return RedirectToAction("Error", "Home");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    tblProvince.ModifyDate = DateTime.Now;

                    _provinceService.Update(tblProvince);

                    TempData["S"] = ErrMsg.Edited;
                    return RedirectToAction(nameof(Edit), new { id = id });
                }
                catch (DbUpdateConcurrencyException e)
                {
                    TempData["W"] = e.Message;
                    return RedirectToAction(nameof(Index));
                }

            }
            return View(tblProvince);
        }

        // GET: Admin/Province/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                TempData["W"] = ErrMsg.NotFound;
                return RedirectToAction("Error", "Home");
            }

            var tblProvince = await _provinceService.GetById(id);

            if (tblProvince == null)
            {
                TempData["W"] = ErrMsg.NotFound;
                return RedirectToAction("Error", "Home");
            }

            return View(tblProvince);
        }

        // POST: Admin/Province/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var tblProvince = await _provinceService.GetById(id);
            if (tblProvince != null)
            {
                TempData["W"] = ErrMsg.NotFound;
                return RedirectToAction("Error", "Home");
            }

            _provinceService.Remove(tblProvince);

            TempData["S"] = ErrMsg.Deleted;
            return RedirectToAction(nameof(Index));
        }

    }
}
