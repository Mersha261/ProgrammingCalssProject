using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PgrogrammingClass.Core.Domain;
using PgrogrammingClass.Sevices.EntitesServices;
using ProgramingCalssProject.Models;

namespace ProgramingCalssProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;

        public CategoryController(ICategoryService categoryService, IProductService productService)
        {
            _categoryService = categoryService;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var model = _categoryService.GetAll().Result.OrderByDescending(a => a.Id);
            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.ParrentCategory = _categoryService.Find(a => a.IsIncludeInTopMenu).ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TblCategory model)
        {          

            ModelState.Remove(nameof(model.Tblproducts));
            if (!ModelState.IsValid)
            {
                TempData["W"] = ErrMsg.ComplateInfo;
                return RedirectToAction(nameof(Create));
            }
            model.CreateDate = DateTime.Now;
            model.ModifyDate = DateTime.Now;
            await _categoryService.Add(model);
            TempData["S"] = ErrMsg.Success;
            return RedirectToAction(nameof(Create));
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.ParrentCategory = _categoryService.Find(a => a.IsIncludeInTopMenu).ToList();
            return View(await _categoryService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TblCategory model)
        {
            ViewBag.ParrentCategory = _categoryService.Find(a => a.IsIncludeInTopMenu).ToList();
            ModelState.Remove(nameof(model.Tblproducts));
            if (!ModelState.IsValid)
            {
                TempData["W"] = ErrMsg.ComplateInfo;
                return View();
            }

            model.ModifyDate = DateTime.Now;
            _categoryService.Update(model);
            TempData["S"] = ErrMsg.Success;
            return RedirectToAction(nameof(Edit), new { id = model.Id });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var model = await _categoryService.GetById(id);
            if (model == null)
            {
                TempData["W"] = ErrMsg.IncorrectInformation;
                return RedirectToAction(nameof(Index));
            }
            if (_categoryService.Find(a => a.ParentCategoryId == id).ToList().Count() > 0)
            {
                TempData["W"] = "این دسته شامل زیر دسته است و شما نمیتوانید آن را حذف کنید،" +
                    " در صورتی که به طور قطع قصد حذف این دسته را دارید،" +
                    " لطفا اول تمامی زیر دسته های این دسته را حذف کرده سپس اقدام نمایید";
                return RedirectToAction(nameof(Index));
            }

            if (_productService.Find(a => a.CategoryId == id).ToList().Count() > 0)
            {
                TempData["W"] = "این دسته شامل محصولاتی است و شما نمیتوانید آن را حذف کنید،" +
                   " در صورتی که به طور قطع قصد حذف این دسته را دارید،" +
                   " لطفا اول تمامی محصولات این دسته را حذف کرده سپس اقدام نمایید";
                return RedirectToAction(nameof(Index));
            }
            _categoryService.Remove(model);
            TempData["S"] = ErrMsg.Deleted;
            return RedirectToAction(nameof(Index));

        }
    }
}
