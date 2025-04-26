using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PgrogrammingClass.Core.Domain;
using PgrogrammingClass.Sevices.EntitesServices;
using ProgramingCalssProject.Models;
using ProgramingCalssProject.Models.Utillity;
using System.Drawing;
using static ProgramingCalssProject.Models.Utillity.ImageProccess;

namespace ProgramingCalssProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BannerController : Controller
    {
        private readonly IBannerService _bannerService;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;


        public BannerController(
            IBannerService bannerService,
            Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _bannerService = bannerService;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _bannerService.GetAll());
        }

        [HttpPost]
        public async Task<IActionResult> Create(TblBanner model, IFormFile Pic)
        {
            ///در اولین مرحله ما چک میکنیم که اطلاعات تایید شده باشند 
            ///چک کردن تصویر، کم کردن حجم تصویر ، چک کردن تایپ تصویر 
            ///ذخیره سازی تصویر 
            ///ذخیره سازی اطلاعات در دیتابیس
            ModelState.Remove(nameof(model.Picture));

            if (!ModelState.IsValid)
            {
                TempData["W"] = ErrMsg.IncorrectInformation;
                return RedirectToAction(nameof(Index));
            }

            if (Pic == null)
            {
                TempData["W"] = "تصویری انتخاب نشده است";
                return RedirectToAction(nameof(Index));
            }

            string picName = Path.GetFileName(Pic.FileName);
            string picExtention = Path.GetExtension(picName).ToLower();

            ImageProccess imageProccess = new ImageProccess();

            var checkPic = imageProccess.CheckPic(Pic, "تصویر بنر");
            if (checkPic.Error)
            {
                TempData["W"] = checkPic.Text;
                return RedirectToAction(nameof(Index));
            }
            if (!Directory.Exists(Path.Combine(_hostingEnvironment.WebRootPath + "/Pic/Banner/")))
            {
                Directory.CreateDirectory(Path.Combine(_hostingEnvironment.WebRootPath + "/Pic/Banner/"));
            }

            string picNameDb = "/Pic/Banner/" + Guid.NewGuid().ToString() + picExtention;

            Bitmap bmp = new Bitmap(Pic.OpenReadStream());
            Image img = imageProccess.ResizeImage(bmp);

            img.Save(Path.Combine(_hostingEnvironment.WebRootPath + picNameDb));
            model.Picture = picNameDb;
            model.CreateDate=DateTime.Now;
            model.ModifyDate = DateTime.Now;
            await _bannerService.Add(model);



            TempData["S"] = ErrMsg.Success;
            return RedirectToAction(nameof(Index));
        }
    }
}
