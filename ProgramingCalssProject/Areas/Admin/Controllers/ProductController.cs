using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PgrogrammingClass.Core.Domain;
using PgrogrammingClass.Data.DataContext;
using ProgramingCalssProject.Models;
using ProgramingCalssProject.Models.Utillity;

namespace ProgramingCalssProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;


        public ProductController(
            ApplicationDbContext context,
            Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Admin/Product
        //stock=1 ==>>True stocke=2 ==>>false
        public async Task<IActionResult> Index(string ProductName = null, int Stock = 0, int PageSize = 20, int PageIndex = 1)
        {
            var model = _context.Tblproduct.Where(a => !a.IsDeleted).Include(t => t.TblCategory).Include(a => a.ProductImages).ToList();
            if (ProductName != null)
            {
                model=model.Where(a=>a.Title.Contains(ProductName)).ToList();
            }
            if(Stock>0)
            {
                if(Stock==1)
                {
                    model = model.Where(a => a.StockQuantity > 0).ToList();
                }
                else
                {
                    model = model.Where(a => a.StockQuantity == 0).ToList();
                }
            }
            var count = model.Count();
            int pageCount = (int)Math.Ceiling((decimal)count / PageSize);
            int skipCount = PageSize * (PageIndex - 1);
            model = model.Skip(skipCount).Take(PageSize).ToList();

            ViewBag.ProductName = ProductName;
            ViewBag.PageIndex = PageIndex;
            ViewBag.PageSize= PageSize;
            ViewBag.PageCount= pageCount;
            return View(model);
        }

        // GET: Admin/Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tblproduct == null)
            {
                return NotFound();
            }

            var tblproduct = await _context.Tblproduct
                .Include(t => t.TblCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblproduct == null)
            {
                return NotFound();
            }

            return View(tblproduct);
        }

        // GET: Admin/Product/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.TblCategory.Where(a => !a.IsIncludeInTopMenu), "Id", "Name");
            return View();
        }

        // POST: Admin/Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tblproduct model, List<IFormFile> pic)
        {
            ModelState.Remove(nameof(model.TblProductComments));
            ModelState.Remove(nameof(model.ProductImages));
            ModelState.Remove(nameof(model.TblCategory));

            ViewData["CategoryId"] = new SelectList(_context.TblCategory.Where(a => !a.IsIncludeInTopMenu), "Id", "Name");

            if (!ModelState.IsValid)
            {
                TempData["W"] = ErrMsg.ComplateInfo;
                return View();
            }
            model.SoldCount = 0;
            model.ViewCount = 0;
            model.CreateDate = DateTime.Now;
            model.ModifyDate = DateTime.Now;
            _context.Add(model);
            await _context.SaveChangesAsync();
            if (pic.Count() > 0)
            {
                foreach (var item in pic)
                {
                    string picName = Path.GetFileName(item.FileName);
                    string picExtention = Path.GetExtension(picName).ToLower();

                    ImageProccess imageProccess = new ImageProccess();

                    var checkPic = imageProccess.CheckPic(item, "تصویر محصول");
                    if (checkPic.Error)
                    {
                        TempData["W"] = checkPic.Text;
                        return RedirectToAction(nameof(Index));
                    }
                    if (!Directory.Exists(Path.Combine(_hostingEnvironment.WebRootPath + "/Pic/Product/")))
                    {
                        Directory.CreateDirectory(Path.Combine(_hostingEnvironment.WebRootPath + "/Pic/Product/"));
                        Directory.CreateDirectory(Path.Combine(_hostingEnvironment.WebRootPath + "/Pic/Product/Thumb"));
                    }
                    string strPicname = Guid.NewGuid().ToString().Replace("-", "");

                    string picNameDb = "/Pic/Product/" + strPicname + picExtention;
                    string picNameDbThumb = "/Pic/Product/Thumb/" + strPicname + picExtention;

                    Bitmap bmp = new Bitmap(item.OpenReadStream());
                    Image img = imageProccess.ResizeImage(bmp);
                    Image thumbnail = imageProccess.GetThumb(bmp);

                    img.Save(Path.Combine(_hostingEnvironment.WebRootPath + picNameDb));
                    thumbnail.Save(Path.Combine(_hostingEnvironment.WebRootPath + picNameDbThumb));

                    TblProductImage tblProductImage = new TblProductImage()
                    {
                        CreateDate = DateTime.Now,
                        ModifyDate = DateTime.Now,
                        Picture = picNameDb,
                        Thumbnaile = picNameDbThumb,
                        ProductId = model.Id,
                        Title = model.Title
                    };
                    _context.Add(tblProductImage);
                    await _context.SaveChangesAsync();

                }
            }

            TempData["S"] = ErrMsg.Success;
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tblproduct == null)
            {
                return NotFound();
            }

            var tblproduct = _context.Tblproduct.Where(a => a.Id == id).Include(a => a.ProductImages).SingleOrDefault();
            if (tblproduct == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.TblCategory, "Id", "Name", tblproduct.CategoryId);
            return View(tblproduct);
        }

        // POST: Admin/Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Title,ShortDescription,FullDescription,MetaDescription,Comment,ShowOnHomepage,KeyWord,ViewCount,SoldCount,StockQuantity,Price,OldPrice,Weight,MetaTitle,CategoryId,Id,CreateDate,ModifyDate")] Tblproduct tblproduct)
        {
            if (id != tblproduct.Id)
            {
                return NotFound();
            }

            ModelState.Remove(nameof(tblproduct.TblProductComments));
            ModelState.Remove(nameof(tblproduct.ProductImages));
            ModelState.Remove(nameof(tblproduct.TblCategory));

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblproduct);

                    await _context.SaveChangesAsync();
                    TempData["S"] = ErrMsg.Edited;
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblproductExists(tblproduct.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.TblCategory, "Id", "Name", tblproduct.CategoryId);
            return View(tblproduct);
        }

        // GET: Admin/Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tblproduct == null)
            {
                return NotFound();
            }

            var tblproduct = await _context.Tblproduct
                .Include(t => t.TblCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblproduct == null)
            {
                return NotFound();
            }

            return View(tblproduct);
        }

        // POST: Admin/Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tblproduct == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Tblproduct'  is null.");
            }
            var tblproduct = await _context.Tblproduct.FindAsync(id);
            if (tblproduct != null)
            {
                tblproduct.IsDeleted = true;
                _context.Tblproduct.Update(tblproduct);
            }

            await _context.SaveChangesAsync();
            TempData["W"] = ErrMsg.Deleted;
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> DeleteImage(int id, int productId)
        {
            var image = _context.TblProductImage.Find(id);
            if (image == null)
            {
                TempData["W"] = ErrMsg.NotFound;
                return RedirectToAction(nameof(Edit), new { id = productId });
            }
            if (_context.TblProductImage.Where(a => a.ProductId == image.ProductId).Count() < 2)
            {
                TempData["W"] = "برای هر محصول باید حداقل یک تصویر وجود داشته باشد و شما نمیتوانید این تصویر را حذف نمایید";
                return RedirectToAction(nameof(Edit), new { id = productId });
            }
            System.IO.File.Delete(_hostingEnvironment.WebRootPath + image.Picture);
            System.IO.File.Delete(_hostingEnvironment.WebRootPath + image.Thumbnaile);

            _context.Remove(image);
            await _context.SaveChangesAsync();

            TempData["S"] = ErrMsg.Deleted;
            return RedirectToAction(nameof(Edit), new { id = productId });
        }

        [HttpPost]
        public async Task<IActionResult> AddImage(List<IFormFile> pic, int ProductId)
        {
            var product = _context.Tblproduct.Find(ProductId);

            if (pic.Count() > 0)
            {
                foreach (var item in pic)
                {
                    string picName = Path.GetFileName(item.FileName);
                    string picExtention = Path.GetExtension(picName).ToLower();

                    ImageProccess imageProccess = new ImageProccess();

                    var checkPic = imageProccess.CheckPic(item, "تصویر محصول");
                    if (checkPic.Error)
                    {
                        TempData["W"] = checkPic.Text;
                        return RedirectToAction(nameof(Index));
                    }
                    if (!Directory.Exists(Path.Combine(_hostingEnvironment.WebRootPath + "/Pic/Product/")))
                    {
                        Directory.CreateDirectory(Path.Combine(_hostingEnvironment.WebRootPath + "/Pic/Product/"));
                        Directory.CreateDirectory(Path.Combine(_hostingEnvironment.WebRootPath + "/Pic/Product/Thumb"));
                    }
                    string strPicname = Guid.NewGuid().ToString().Replace("-", "");

                    string picNameDb = "/Pic/Product/" + strPicname + picExtention;
                    string picNameDbThumb = "/Pic/Product/Thumb/" + strPicname + picExtention;

                    Bitmap bmp = new Bitmap(item.OpenReadStream());
                    Image img = imageProccess.ResizeImage(bmp);
                    Image thumbnail = imageProccess.GetThumb(bmp);

                    img.Save(Path.Combine(_hostingEnvironment.WebRootPath + picNameDb));
                    thumbnail.Save(Path.Combine(_hostingEnvironment.WebRootPath + picNameDbThumb));

                    TblProductImage tblProductImage = new TblProductImage()
                    {
                        CreateDate = DateTime.Now,
                        ModifyDate = DateTime.Now,
                        Picture = picNameDb,
                        Thumbnaile = picNameDbThumb,
                        ProductId = product.Id,
                        Title = product.Title
                    };
                    _context.Add(tblProductImage);
                    await _context.SaveChangesAsync();

                }
            }

            TempData["S"] = ErrMsg.Success;
            return RedirectToAction(nameof(Edit), new { id = ProductId });
        }


        private bool TblproductExists(int id)
        {
            return (_context.Tblproduct?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
