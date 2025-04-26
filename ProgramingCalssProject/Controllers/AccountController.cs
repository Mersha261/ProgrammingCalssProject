using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PgrogrammingClass.Core.Domain;
using PgrogrammingClass.Data.DataContext;
using ProgramingCalssProject.Models;
using ProgramingCalssProject.Models.Utillity;
using ProgramingCalssProject.Models.ViewModel;

namespace ProgramingCalssProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IpersianDateTime _dt;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;


        public AccountController(
            ApplicationDbContext context,
            IpersianDateTime dt,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _dt = dt;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [Authorize]
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);

            return View(currentUser);
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Index(ApplicationUser model, int Year, int Month, int Day)
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);

            if (Year > 0 && Month > 0 && Day > 0)
            {
                currentUser.Birthday = _dt.Gregorian(Year, Month, Day, 0, 0);
            }
            currentUser.LastIpAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            currentUser.Name = model.Name;
            currentUser.Gender = model.Gender;
            currentUser.Email = model.Email;
            currentUser.Family = model.Family;

            _context.Update(currentUser);
            await _context.SaveChangesAsync();

            return View(currentUser);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["W"] = ErrMsg.ComplateInfo;
                return View();
            }

            Random rnd = new Random();

            ApplicationUser applicationUser = new ApplicationUser()
            {
                LastIpAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString(),
                Birthday = _dt.Gregorian(model.Year, model.Month, model.Day, 0, 0),
                CodeCounter = 1,
                MobileConfirmCode = rnd.Next(1000, 9999),
                Email = model.Email,
                SendDate = DateTime.Now,
                CreateDate = DateTime.Now,
                EmailConfirmed = false,
                Family = model.Family,
                Gender = model.Gender,
                Name = model.Name,
                ModifyDate = DateTime.Now,
                NormalizedEmail = model.Email.ToUpper(),
                UserName = model.PhoneNumber,
                PhoneNumberConfirmed = false,
                NormalizedUserName = model.PhoneNumber.ToUpper(),
                PhoneNumber = model.PhoneNumber,
                TwoFactorEnabled = false
            };

            var registerResult = await _userManager.CreateAsync(applicationUser, model.PasswordHash);
            if (!registerResult.Succeeded)
            {
                TempData["W"] = registerResult.Errors;
                return View();
            }
            await _userManager.AddToRoleAsync(applicationUser, "USER");

            var sendsms = new SendSms();

            ActivationCodeModel activationCodeModel = new ActivationCodeModel()
            {
                AccessHash = " 1c738e0e - 4f10 - 4299 - bdaa - 1cff6eb84908",
                PatternId = "1825564654",
                token1 = model.Name,
                Mobile = model.PhoneNumber,
                UserGroupID = "23132123",
                SendDateInTimeStamp = 1,
                username = "231313",
                password = "231321",
            };


            var result = sendsms.SendSmsViaRayeganSms(activationCodeModel);
            if (result != "Ok")
            {
                TempData["W"] = result;
                return View();
            }
            ///در این قسمت کد های مربوط به ارسال ایمیل تایید را بنویسید

            TempData["S"] = "ثبت نام شما موفقیت آمیز بود، لطفا به بخش تایید موبایل رفته و شماره موبایل خود را تایید نمایید";

            return RedirectToAction(nameof(ConfirmMobile));
        }

        public IActionResult ConfirmMobile()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ConfirmMobile(ConfirmMobileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["W"] = ErrMsg.ComplateInfo;
                return View();
            }
            var user = _context.TblUser.Where(a => a.UserName == model.PhoneNumber).SingleOrDefault();
            if (user == null)
            {
                TempData["W"] = ErrMsg.IncorrectInformation;
                return View();
            }
            if (user.PhoneNumberConfirmed)
            {
                TempData["W"] = "شماره موبایل شما قبلا تایید شده است، لطفا از بخش ورورد وارد سامانه شوید";
                return View();
            }
            if (model.Code != user.MobileConfirmCode)
            {
                TempData["W"] = "کد تایید وارد شده صحیح نمیباشد، لطفا مجددا اقدام نمایید";
                return View();
            }

            user.PhoneNumberConfirmed = true;

            _context.Update(user);
            await _context.SaveChangesAsync();


            TempData["S"] = "شماره شما با موفقیت تایید شد";
            return View();
        }


        public IActionResult LogIn()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> LogIn(LogInViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["W"] = ErrMsg.ComplateInfo;
                return View();
            }
            var user = _userManager.Users.Where(a => a.UserName == model.PhoneNumber).SingleOrDefault();

            if (user == null)
            {
                TempData["W"] = ErrMsg.IncorrectInformation;
                return View();
            }

            if (!user.PhoneNumberConfirmed)
            {
                TempData["W"] = "شماره موبایل شما تایید نشده است، لطفا شماره خود را تایید نمایید";
                return RedirectToAction(nameof(ConfirmMobile));
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var cookies = HttpContext.Request.Cookies;
                string cookiBaseket = cookies.Where(a => a.Key == "ProgrammingCalssShoppingcart").SingleOrDefault().Value;
                if (cookiBaseket == null)
                {
                    TempData["S"] = "شما با موفقیت وارد سامانه شدید";
                    return RedirectToAction("Index", "Home");
                }

                var shoppingcart = _context.TblShoppingcart.Where(a => a.Cookie == cookiBaseket)
                    .Include(a => a.TblShoppingCartDetails)
                    .SingleOrDefault();
                if (shoppingcart == null)
                {
                    TempData["S"] = "شما با موفقیت وارد سامانه شدید";
                    return RedirectToAction("Index", "Home");
                }

                shoppingcart.UserId = user.Id;
                _context.Update(shoppingcart);
                await _context.SaveChangesAsync();

                TempData["S"] = "شما با موفقیت وارد سامانه شدید";
                return RedirectToAction("Index", "Home");
            }

            TempData["W"] = "نام کاربری یا کلمه عبور شما صحیح نمیباشد";
            return View();
        }


        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        [Authorize]
        public async Task<IActionResult> ChengePass()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChengePass(ChangePass model)
        {
            if (!ModelState.IsValid)
            {
                TempData["W"] = ErrMsg.ComplateInfo;
                return View();
            }

            var currentUser= await _userManager.GetUserAsync(User);
            var result=await _userManager.ChangePasswordAsync(currentUser, model.OldPassword, model.NewPassword);   

            if (!result.Succeeded)
            {
                TempData["W"] = result.Errors;
                return View();
            }
            
            await _signInManager.RefreshSignInAsync(currentUser);

            TempData["S"] = "کلمه عبور شما با موفقیت تغییر کرد";

            return View();
        }


        #region Json

        [HttpPost]
        public JsonResult DuplicateMobile(string PhoneNumber)
        {
            if (PhoneNumber == null || PhoneNumber == string.Empty)
                return Json(false);

            if (_context.TblUser.Where(a => a.PhoneNumber == PhoneNumber).SingleOrDefault() != null)
                return Json(false);

            return Json(true);
        }

        [HttpPost]
        public JsonResult DuplicateEmail(string Email)
        {
            if (Email == null || Email == string.Empty)
                return Json(false);

            if (_context.TblUser.Where(a => a.Email == Email).SingleOrDefault() != null)
                return Json(false);

            return Json(true);
        }
        #endregion



    }
}
