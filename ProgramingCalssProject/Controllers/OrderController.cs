using MellatPayment1;
using MellatPayment2ProgrammingClass;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using PgrogrammingClass.Core.Domain;
using PgrogrammingClass.Data.DataContext;
using ProgramingCalssProject.Models;
using ProgramingCalssProject.Models.Getdata;
using static MellatPayment1.SaleServiceSoapClient;

namespace ProgramingCalssProject.Controllers
{
    public class OrderController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IGetData _getData;



        public OrderController(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context,
            IGetData getData)
        {
            _userManager = userManager;
            _context = context;
            _getData = getData;
        }


        public async Task<IActionResult> Index()
        {
            var cookies = HttpContext.Request.Cookies;
            string cookiBaseket = cookies.Where(a => a.Key == "ProgrammingCalssShoppingcart").SingleOrDefault().Value;
            if (cookiBaseket == null)
            {
                TempData["W"] = "سبد خرید شما خالی است";
                return RedirectToAction("Index", "Home");
            }

            var shoppingcart = _context.TblShoppingcart.Where(
                a => a.Cookie == cookiBaseket
                && !a.IsPaied
                && !a.IsSentToBank)
                .Include(a => a.TblShoppingCartDetails)
                .Include(a => a.TblUserAddress)
                .SingleOrDefault();
            if (shoppingcart == null)
            {
                Response.Cookies.Delete("ProgrammingCalssShoppingcart");
                TempData["W"] = "سبد خرید شما خالی است";
                return RedirectToAction("Index", "Home");
            }

            shoppingcart.TotalPrice = shoppingcart.TblShoppingCartDetails.Sum(a => a.PriceWithOffCopon);
            shoppingcart.PriceWithoutOff = shoppingcart.TblShoppingCartDetails.Sum(a => a.Price);

            _context.Update(shoppingcart);
            await _context.SaveChangesAsync();

            int totalOff = (shoppingcart.TotalPrice * shoppingcart.OffPercent) / 100;
            shoppingcart.TotalPrice = shoppingcart.TotalPrice - totalOff;

            _context.Update(shoppingcart);
            await _context.SaveChangesAsync();

            return View(shoppingcart);
        }

        [HttpPost]
        public async Task<string> AddTocart([FromBody] int id)
        {

            var product = _context.Tblproduct.Where(a => a.Id == id).SingleOrDefault();
            if (product == null)
            {
                return "محصولی یافت نشد";

            }
            if (product.StockQuantity == 0)
            {
                return "موجودی انبار این محصول به اتمام رسیده است";
            }
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            if (currentUser == null)
            {
                var cookies = HttpContext.Request.Cookies;
                string cookiBaseket = cookies.Where(a => a.Key == "ProgrammingCalssShoppingcart").SingleOrDefault().Value;
                if (cookiBaseket == null)
                {
                    var cookiOpt = new CookieOptions()
                    {
                        Expires = DateTime.Now.AddMonths(3)
                    };
                    var cookivalue = Guid.NewGuid().ToString().Replace("-", "");
                    HttpContext.Response.Cookies.Append("ProgrammingCalssShoppingcart", cookivalue, cookiOpt);
                    TblShoppingcart tblShoppingcart = new TblShoppingcart()
                    {
                        Cookie = cookivalue,
                        IsCoponSet = false,
                        CreateDate = DateTime.Now,
                        IsSentToBank = false,
                        IsPaied = false,
                        IsSentToUser = false,
                        Id = Guid.NewGuid().ToString()
                    };
                    _context.Add(tblShoppingcart);
                    _context.SaveChanges();

                    TblShoppingCartDetails tblShoppingCartDetails = new TblShoppingCartDetails()
                    {
                        Count = 1,
                        Price = product.Price,
                        PriceWithOffCopon = product.Price,
                        ShoppingCartId = tblShoppingcart.Id,
                        ProductId = id,
                        ProductName = product.Title,
                        Weight = product.Weight,
                        Id = Guid.NewGuid().ToString()
                    };
                    _context.Add(tblShoppingCartDetails);
                    _context.SaveChanges();

                    return "محصول با موفقیت به سبد خرید اضافه شد";
                }

                var currentShoppingCart = _context.TblShoppingcart.Where(a => a.Cookie == cookiBaseket).Include(a => a.TblShoppingCartDetails).SingleOrDefault();
                if (currentShoppingCart == null)
                {
                    return "مشکلی در ثبت اطلاعات پیش امده است";
                }

                if (currentShoppingCart.TblShoppingCartDetails.Where(a => a.ProductId == id).SingleOrDefault() != null)
                {
                    return "این محصول قبلا به سبد خرید شما اضافه شده است ";
                }

                TblShoppingCartDetails tblShoppingCartDetails2 = new TblShoppingCartDetails()
                {
                    Count = 1,
                    Price = product.Price,
                    PriceWithOffCopon = product.Price,
                    ShoppingCartId = currentShoppingCart.Id,
                    ProductId = id,
                    ProductName = product.Title,
                    Weight = product.Weight,
                    Id = Guid.NewGuid().ToString()
                };
                _context.Add(tblShoppingCartDetails2);
                _context.SaveChanges();
                return "محصول با موفقیت به سبد خرید اضافه شد";
            }

            var userShoppingcart = _context.TblShoppingcart.Where(a => a.IsPaied && a.UserId == currentUser.Id).SingleOrDefault();
            if (userShoppingcart == null)
            {
                var cookiOpt = new CookieOptions()
                {
                    Expires = DateTime.Now.AddMonths(3)
                };
                var cookivalue = Guid.NewGuid().ToString().Replace("-", "");
                HttpContext.Response.Cookies.Append("ProgrammingCalssShoppingcart", cookivalue, cookiOpt);
                TblShoppingcart tblShoppingcart = new TblShoppingcart()
                {
                    Cookie = cookivalue,
                    IsCoponSet = false,
                    CreateDate = DateTime.Now,
                    IsSentToBank = false,
                    IsPaied = false,
                    IsSentToUser = false,
                    UserId = currentUser.Id,
                    Id = Guid.NewGuid().ToString()
                };
                _context.Add(tblShoppingcart);
                _context.SaveChanges();

                TblShoppingCartDetails tblShoppingCartDetails = new TblShoppingCartDetails()
                {
                    Count = 1,
                    Price = product.Price,
                    PriceWithOffCopon = product.Price,
                    ShoppingCartId = tblShoppingcart.Id,
                    ProductId = id,
                    ProductName = product.Title,
                    Weight = product.Weight,
                    Id = Guid.NewGuid().ToString()
                };
                _context.Add(tblShoppingCartDetails);
                _context.SaveChanges();
                return "محصول با موفقیت به سبد خرید اضافه شد";
            }

            TblShoppingCartDetails tblShoppingCartDetails3 = new TblShoppingCartDetails()
            {
                Count = 1,
                Price = product.Price,
                PriceWithOffCopon = product.Price,
                ShoppingCartId = userShoppingcart.Id,
                ProductId = id,
                ProductName = product.Title,
                Weight = product.Weight,
                Id = Guid.NewGuid().ToString()
            };
            _context.Add(tblShoppingCartDetails3);
            _context.SaveChanges();
            return "محصول با موفقیت به سبد خرید اضافه شد";
        }

        [HttpPost]
        public PartialViewResult GetShoppingcartData()
        {
            var cookies = HttpContext.Request.Cookies;
            string cookiBaseket = cookies.Where(a => a.Key == "ProgrammingCalssShoppingcart").SingleOrDefault().Value;
            if (cookiBaseket == null)
                return null;

            var shoppingcart = _context.TblShoppingcart.Where(a => a.Cookie == cookiBaseket)
                .Include(a => a.TblShoppingCartDetails)
                .SingleOrDefault();
            if (shoppingcart == null)
                return null;

            return PartialView("_ShoppingcartPartial", shoppingcart.TblShoppingCartDetails);

        }

        [HttpPost]
        public async Task<IActionResult> AddCount(int count, string id)
        {
            if (count <= 0)
            {
                TempData["W"] = "تعداد نمیتواند کمتر از 1 باشد";
                return RedirectToAction(nameof(Index));
            }
            var shoppingCartdetails = _context.TblShoppingCartDetails.Where(a => a.Id == id).SingleOrDefault();
            if (shoppingCartdetails == null)
            {
                TempData["W"] = ErrMsg.IncorrectInformation;
                return RedirectToAction(nameof(Index));
            }
            var product = _context.Tblproduct.Where(a => a.Id == shoppingCartdetails.ProductId).SingleOrDefault();

            if (product.StockQuantity < count)
            {

                TempData["W"] = "تعداد وارد شده از موجودی انبار بیشتر است، تعداد موجودی انبار این محصول " + product.StockQuantity + " است ";
                return RedirectToAction(nameof(Index));
            }


            shoppingCartdetails.Count = count;
            _context.Update(shoppingCartdetails);
            await _context.SaveChangesAsync();

            TempData["S"] = ErrMsg.Edited;
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> AddCopon(string copon)
        {
            var cookies = HttpContext.Request.Cookies;
            string cookiBaseket = cookies.Where(a => a.Key == "ProgrammingCalssShoppingcart").SingleOrDefault().Value;
            if (cookiBaseket == null)
                return View();

            var shoppingcart = _context.TblShoppingcart.Where(
                a => a.Cookie == cookiBaseket
                && !a.IsPaied
                && !a.IsSentToBank)
                .Include(a => a.TblShoppingCartDetails)
                .Include(a => a.TblUserAddress)
                .SingleOrDefault();

            if (shoppingcart == null)
            {
                TempData["W"] = ErrMsg.IncorrectInformation;
                return RedirectToAction(nameof(Index));
            }
            var tblCopon = _context.TblOffCopon.Where(a => a.CoponName == copon).SingleOrDefault();

            if (tblCopon == null)
            {
                TempData["W"] = "کد تخفیف وارد شده صحیح نیست";
                return RedirectToAction(nameof(Index));
            }

            shoppingcart.OffPercent = tblCopon.Percent;
            shoppingcart.IsCoponSet = true;
            shoppingcart.OffCopon = tblCopon.CoponName;
            _context.Update(shoppingcart);
            await _context.SaveChangesAsync();

            TempData["S"] = "کد تخفیف شما با موفقیت اعمال گردید ";
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> AddAddress(int addressId)
        {
            var cookies = HttpContext.Request.Cookies;
            string cookiBaseket = cookies.Where(a => a.Key == "ProgrammingCalssShoppingcart").SingleOrDefault().Value;
            if (cookiBaseket == null)
                return View();

            var shoppingcart = _context.TblShoppingcart.Where(
                a => a.Cookie == cookiBaseket
                && !a.IsPaied
                && !a.IsSentToBank)
                .Include(a => a.TblShoppingCartDetails)
                .Include(a => a.TblUserAddress)
                .SingleOrDefault();

            if (shoppingcart == null)
            {
                TempData["W"] = ErrMsg.IncorrectInformation;
                return RedirectToAction(nameof(Index));
            }
            var tblAddress = _context.TblUserAddress.Where(a => a.Id == addressId).SingleOrDefault();

            if (tblAddress == null)
            {
                TempData["W"] = "آدرس وارد شده صحیح نیست";
                return RedirectToAction(nameof(Index));
            }
            var currentuser = await _userManager.GetUserAsync(HttpContext.User);

            if (tblAddress.UserId.ToString() != currentuser.Id.ToString())
            {
                TempData["W"] = "ادرس وارد شده صحیح نیست";
                return RedirectToAction(nameof(Index));
            }

            shoppingcart.AddressId = addressId;
            _context.Update(shoppingcart);
            await _context.SaveChangesAsync();
            TempData["S"] = "آدرس با موفقیت ثبت گردید";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> AddShippingWay(string shippingWay)
        {

            var cookies = HttpContext.Request.Cookies;
            string cookiBaseket = cookies.Where(a => a.Key == "ProgrammingCalssShoppingcart").SingleOrDefault().Value;
            if (cookiBaseket == null)
                return View();

            var shoppingcart = _context.TblShoppingcart.Where(
                a => a.Cookie == cookiBaseket
                && !a.IsPaied
                && !a.IsSentToBank)
                .Include(a => a.TblShoppingCartDetails)
                .Include(a => a.TblUserAddress)
                .ThenInclude(a => a.TblCity)
                .SingleOrDefault();

            if (shoppingcart == null)
            {
                TempData["W"] = ErrMsg.IncorrectInformation;
                return RedirectToAction(nameof(Index));
            }

            var shippngList = shippingWay.Split(',');
            int price = Convert.ToInt32(shippngList[0]);
            string shippingPostWay = shippngList[1];

            ///نکته بسیار مهم ، در این قسمت حتما ما باید چک کنیم که روش ارسال انتخابی مشتری و قیمتی که در حال حاضر داریم
            ///با قیمت واقعی حتما یکی باشند و مشتری تخلفی انجام نداده باشد

            var shippingcalculate =
                _getData.GetShippingWay(
                shoppingcart.TblUserAddress.TblCity.ProvinceId,
                shoppingcart.TblShoppingCartDetails.Sum(a => a.Count * (int)a.Weight));

            if (shippingPostWay.Contains("پست سفارشی"))
            {
                if (shippingcalculate.Where(a => a.ShippingName.Contains("پست سفارشی")).SingleOrDefault().ShippingPrice != price)
                {
                    TempData["W"] = "هزینه پستی وارد شده معتبر نیست";
                    return RedirectToAction(nameof(Index));
                }
            }
            if (shippingPostWay.Contains("پست پیشتاز"))
            {
                if (shippingcalculate.Where(a => a.ShippingName.Contains("پست پیشتاز")).SingleOrDefault().ShippingPrice != price)
                {
                    TempData["W"] = "هزینه پستی وارد شده معتبر نیست";
                    return RedirectToAction(nameof(Index));
                }
            }

            shoppingcart.ShippingPrice = price;
            shoppingcart.ShippingWayName = shippingPostWay;
            _context.Update(shoppingcart);
            await _context.SaveChangesAsync();

            TempData["S"] = "شیوه ارسال شما ثبت گردید";
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> Pay(string shoppingcartId)
        {
            var cookies = HttpContext.Request.Cookies;
            string cookiBaseket = cookies.Where(a => a.Key == "ProgrammingCalssShoppingcart").SingleOrDefault().Value;
            if (cookiBaseket == null)
                return View();

            var shoppingcart = _context.TblShoppingcart.Where(
                a => a.Cookie == cookiBaseket
                && !a.IsPaied
                && !a.IsSentToBank)
                .Include(a => a.TblShoppingCartDetails)
                .Include(a => a.TblUserAddress)
                .SingleOrDefault();

            if (shoppingcart == null)
            {
                TempData["W"] = ErrMsg.IncorrectInformation;
                return RedirectToAction(nameof(Index));
            }
            var checkShoppingCart = await _context.TblShoppingcart.Where(a => a.Id == shoppingcartId).SingleOrDefaultAsync();

            if (checkShoppingCart == null)
            {
                TempData["W"] = ErrMsg.IncorrectInformation;
                return RedirectToAction(nameof(Index));
            }
            if (checkShoppingCart.Id != shoppingcart.Id)
            {
                TempData["W"] = ErrMsg.IncorrectInformation;
                return RedirectToAction(nameof(Index));
            }
            foreach (var item in shoppingcart.TblShoppingCartDetails)
            {
                var product = _context.Tblproduct.Where(a => a.Id == item.ProductId).SingleOrDefault();
                if (product.StockQuantity == 0)
                {
                    TempData["W"] = "موجودی انبار " + product.Title + " صفر است، لطفا محصول را از سبد خرید خود حذف کرده و مجددا اقدام به پرداخت نمایید";
                    return RedirectToAction(nameof(Index));
                }
            }
            ///در گاه پرداخت ها همیشه از شما قیمت را به صورت ریال دریافت میکنند، بنابراین اگر در وب سایت شما قیمت ها به تومان ارائه میشود باید در 10 ضرب نمایید

            int totalPrice = (shoppingcart.TotalPrice + shoppingcart.ShippingPrice) * 10;

            ///چهار متغیر زیر را باید از بانک دریافت نمایید

            ///ای دی درگاه پرداخت فروشگاه ما
            string terminalId = "65465456564";

            string username = "mersha";
            string password = "123456789";

            ///شناسه پرداخت کننده که صاحب پروژه از بانک میگیرد
            string PayerId = "23165465";
            int amount = totalPrice;
            DateTime dt = DateTime.Now;

            string date = string.Format("{0}{1}{2}", dt.Year, dt.Month, dt.Day);
            string time = string.Format("{0}{1}{2}", dt.Hour, dt.Minute, dt.Second);
            string localDate = date;
            string localTime = time;

            /// هر متنی حداکثر تا 1000 کاراکتر که طراح مایل به حفظ آنها برای هر تراکنش است
            string additionalData = shoppingcart.Id.ToString();

            string callBackUrl = "Https://www.DOMAINNAME.ir/Order/ComeBackFromBank/";
            var baseUrl = "https://pec.shaparak.ir/NewIPG/";

            //از این قسمت تمامی کد ها طبق مستندات درگاه پرداخت بانک ملت است

            Random rnd = new Random();

            shoppingcart.CustomOrderNumber = rnd.Next(10000, 922337204).ToString();

            while (_context.TblShoppingcart.Where(a => a.CustomOrderNumber == shoppingcart.CustomOrderNumber).SingleOrDefault() != null)
            {
                shoppingcart.CustomOrderNumber = rnd.Next(10000, 922337204).ToString();
            }

            SaleServiceSoapClient saleServiceSoapClient = new SaleServiceSoapClient(EndpointConfiguration.SaleServiceSoap);
            ClientSaleRequestData clientSaleRequestData = new ClientSaleRequestData()
            {
                AdditionalData = additionalData,
                Amount = amount,
                LoginAccount = PayerId,
                OrderId = Convert.ToInt64(shoppingcart.CustomOrderNumber),
                CallBackUrl = callBackUrl,
                Originator = ""
            };

            SalePaymentRequestRequestBody salePaymentRequestRequestBody = new SalePaymentRequestRequestBody(clientSaleRequestData);

            SalePaymentRequestRequest salePaymentRequestRequest = new SalePaymentRequestRequest(salePaymentRequestRequestBody);

            var status = await saleServiceSoapClient.SalePaymentRequestAsync(clientSaleRequestData);

            string errors = null;

            if (status.Body.SalePaymentRequestResult.Status != 0)
            {
                switch (status.Body.SalePaymentRequestResult.Status)
                {
                    case -32768:
                        errors = " خطاي ناشناخته رخ داده است";
                        break;

                    case -1552:
                        errors = "برگشت تراکنش مجاز نمی باشد";
                        break;
                    case -1551:
                        errors = "برگشت تراکنش قبلا اًنجام شده است ";
                        break;
                    case -1550:
                        errors = "  برگشت تراکنش در وضعیت جاري امکان پذیر نمی باشد ";
                        break;
                    case -1549:
                        errors = "زمان مجاز براي درخواست برگشت تراکنش به اتمام رسیده است ";
                        break;

                    case -1548:
                        errors = "فراخوانی سرویس درخواست پرداخت قبض  ناموفق بود";
                        break;
                    case -1540:
                        errors = "تایید تراکنش ناموفق می باشد ";
                        break;
                    case -1536:
                        errors = "فراخوانی سرویس درخواست شارژ تاپ آپ ناموفق بود";
                        break;
                    case -1533:
                        errors = "تراکنش قبلاً تایید شده است";
                        break;

                    case -1532:
                        errors = "تراکنش از سوي پذیرنده تایید شد";
                        break;
                    case -1531:
                        errors = "تایید تراکنش ناموفق امکان پذیر نمی باشد  ";
                        break;
                    case -1530:
                        errors = "پذیرنده مجاز به تایید این تراکنش نمی باشد";
                        break;

                    case -1528:
                        errors = "اطلاعات پرداخت یافت نشد";
                        break;
                    case -1527:
                        errors = "انجام عملیات درخواست پرداخت تراکنش خرید ناموفق بود";
                        break;

                    case -1507:
                        errors = "تراکنش برگشت به سوئیچ ارسال شد";
                        break;

                    case -1505:
                        errors = "تایید تراکنش توسط پذیرنده انجام شد ";
                        break;

                    case -138:
                        errors = "عملیات پرداخت توسط کاربر لغو شد ";
                        break;

                    case -132:
                        errors = "مبلغ تراکنش کمتر از حداقل مجاز میباشد  ";
                        break;
                    case -131:
                        errors = "نامعتبر می باشد Token  ";
                        break;
                    case -130:
                        errors = "زمان منقضی شده است Token";
                        break;
                    case -128:
                        errors = "معتبر نمی باشد IP قالب آدرس";
                        break;

                    case -127:
                        errors = "آدرس اینترنتی معتبر نمی باشد";
                        break;
                    case -126:
                        errors = "کد شناسایی پذیرنده معتبر نمی باشد  ";
                        break;
                    case -121:
                        errors = "رشته داده شده بطور کامل عددي نمی باشد ";
                        break;
                    case -120:
                        errors = "طول داده ورودي معتبر نمی باشد";
                        break;
                    case -119:
                        errors = "سازمان نامعتبر می باشد ";
                        break;
                    case -118:
                        errors = "مقدار ارسال شده عدد نمی باشد";
                        break;

                    case -117:
                        errors = "طول رشته کم تر از حد مجاز می باشد";
                        break;

                    case -116:
                        errors = "طول رشته بیش از حد مجاز می باشد";
                        break;
                    case -115:
                        errors = "شناسه پرداخت نامعتبر می باشد";
                        break;

                    case -114:
                        errors = "شناسه قبض نامعتبر می باشد";
                        break;

                    case -113:
                        errors = "پارامتر ورودي خالی می باشد ";
                        break;
                    case -112:
                        errors = "شماره سفارش تکراري است ";
                        break;

                    case -111:
                        errors = "مبلغ تراکنش بیش از حد مجاز پذیرنده می باشد";
                        break;
                    case -108:
                        errors = "قابلیت برگشت تراکنش براي پذیرنده غیر فعال می باشد";
                        break;
                    case -107:
                        errors = " قابلیت ارسال تاییده تراکنش براي پذیرنده غیر فعال می باشد";
                        break;
                    case -106:
                        errors = "قابلیت شارژ براي پذیرنده غیر فعال می باشد";
                        break;
                    case -105:
                        errors = "قابلیت تاپ آپ براي پذیرنده غیر فعال می باشد";
                        break;
                    case -104:
                        errors = "قابلیت پرداخت قبض براي پذیرنده غیر فعال می باشد";
                        break;
                    case -103:
                        errors = "قابلیت خرید براي پذیرنده غیر فعال می باشد ";
                        break;
                    case -102:
                        errors = "تراکنش با موفقیت برگشت داده شد ";
                        break;
                    case -101:
                        errors = "پذیرنده اهراز هویت نشد ";
                        break;
                    case -100:
                        errors = "پذیرنده غیرفعال می باشد";
                        break;
                    case -1:
                        errors = "خطاي سرور Server Error ";
                        break;
                    case 1:
                        errors = "صادرکننده ي کارت از انجام تراکنش صرف نظر کرد";
                        break;
                    case 2:
                        errors = "عملیات تاییدیه این تراکنش قبلا باموفقیت  صورت پذیرفته است";
                        break;
                    case 3:
                        errors = "پذیرنده ي فروشگاهی نامعتبر می باشد ";
                        break;
                    case 5:
                        errors = "از انجام تراکنش صرف نظر شد ";
                        break;
                    case 6:
                        errors = "بروز خطایی ناشناخته ";
                        break;

                    case 8:
                        errors = "باتشخیص هویت دارنده ي کارت، تراکنش موفق می باشد";
                        break;
                    case 9:
                        errors = "درخواست رسیده در حال پی گیري و انجام است";
                        break;
                    case 10:
                        errors = "تراکنش با مبلغی پایین تر از مبلغ درخواستی  )کمبود حساب مشتري(پذیرفته شده است";
                        break;
                    case 12:
                        errors = "تراکنش نامعتبر است";
                        break;
                    case 13:
                        errors = "مبلغ تراکنش نادرست است";
                        break;
                    case 14:
                        errors = "شماره کارت ارسالی نامعتبر است(وجود ندارد)";
                        break;
                    case 15:
                        errors = "صادرکننده ي کارت نامعتبراست(وجود ندارد)";
                        break;

                    case 17:
                        errors = "مشتري درخواست کننده حذف شده است Customer Cancellation ";
                        break;

                    case 20:
                        errors = "در موقعیتی که سوئیچ جهت پذیرش تراکنش نیازمند پرس و جو از کارت است ممکن است درخواست از کارت(ترمینال) بنماید این پیام مبین نامعتبر بودن جواب است";
                        break;
                    case 21:
                        errors = "خطا در ارسال به درگاه پرداخت: کد خطا 21";
                        break;
                    case 22:
                        errors = "تراکنش مشکوك به بد عمل کردن(کارت، ترمینال ، دارنده کارت) بوده است لذا پذیرفته نشده است";
                        break;
                    case 30:
                        errors = "قالب پیام داراي اشکال است  ";
                        break;
                    case 31:
                        errors = "پذیرنده توسط سوئی پشتیبانی نمی شود  ";
                        break;
                    case 32:
                        errors = "تراکنش به صورت غیر قطعی کامل شده است Completed Partially ";
                        break;
                    case 33:
                        errors = "تاریخ انقضاي کارت سپري شده است  ";
                        break;
                    case 38:
                        errors = "تعداد دفعات ورود رمزغلط بیش از حدمجاز است";
                        break;
                    case 39:
                        errors = "کارت حساب اعتباري ندارد";
                        break;

                    case 40:
                        errors = "عملیات درخواستی پشتیبانی نمی گردد ";
                        break;
                    case 41:
                        errors = "کارت مفقودي می باشد  ";
                        break;
                    case 43:
                        errors = "کارت مسروقه می باشد  ";
                        break;
                    case 45:
                        errors = "قبض قابل پرداخت نمی باشد ";
                        break;
                    case 51:
                        errors = "موجودي کافی نمی باشد ";
                        break;
                    case 54:
                        errors = "تاریخ انقضاي کارت سپري شده است ";
                        break;
                    case 55:
                        errors = "رمز کارت نا معتبر است";
                        break;
                    case 56:
                        errors = "کارت نا معتبر است ";
                        break;
                    case 57:
                        errors = "انجام تراکنش مربوطه توسط دارنده ي  کارت مجاز نمی باشد";
                        break;
                    case 58:
                        errors = "انجام تراکنش مربوطه توسط پایانه ي انجام دهنده مجاز نمی باشد";
                        break;
                    case 59:
                        errors = "کارت مظنون به تقلب است ";
                        break;
                    case 61:
                        errors = "مبلغ تراکنش بیش از حد مجاز می باشد ";
                        break;
                    case 62:
                        errors = "کارت محدود شده است ";
                        break;
                    case 63:
                        errors = "تمهیدات امنیتی نقضگردیده است  ";
                        break;
                    case 65:
                        errors = "تعداد درخواست تراکنش بیش از حد مجاز می باشد";
                        break;
                    case 68:
                        errors = "پاسخ لازم براي تکمیل یا انجام تراکنش خیلی دیر رسیده است";
                        break;
                    case 69:
                        errors = "تعداد دفعات تکرار رمز از حد مجاز گذشته است";
                        break;
                    case 75:
                        errors = "تعداد دفعات ورود رمزغلط بیش از حدمجاز است";
                        break;
                    case 78:
                        errors = "کارت فعال نیست ";
                        break;
                    case 79:
                        errors = "حساب متصل به کارت نا معتبر است یا داراي اشکال است";
                        break;
                    case 80:
                        errors = "درخواست تراکنش رد شده است ";
                        break;
                    case 81:
                        errors = "کارت پذیرفته نشد ";
                        break;
                    case 83:
                        errors = "سرویس دهنده سوئیچ کارت تراکنش را نپذیرفته است";
                        break;
                    case 84:
                        errors = " Issuer Down-Slm";
                        break;
                    case 91:
                        errors = "سیستم صدور مجوز انجام تراکنش موقتا غیر  فعال است و یا زمان تعیین شده براي صدو مجوز به پایان رسیده است";
                        break;
                    case 92:
                        errors = " مقصد تراکنش پیدا نشد";
                        break;
                    case 93:
                        errors = "امکان تکمیل تراکنش وجود ندارد ";
                        break;
                }

                if (errors != null)
                {
                    TempData["W"] = errors;
                    return RedirectToAction(nameof(Index));
                }
            }
            Dictionary<string, string> parametrs = new Dictionary<string, string>()
            {
                ["Token"] = status.Body.SalePaymentRequestResult.Token.ToString()
            };

            var url = QueryHelpers.AddQueryString(baseUrl, parametrs);
            HttpContext.Response.Redirect(url);
            shoppingcart.IsSentToBank = true;
            _context.Update(shoppingcart);
            await _context.SaveChangesAsync();

            return Redirect(url);
        }

        [HttpPost]
        public async Task<IActionResult> ComeBackFromBank(long Token, string status, string OrderId, string TerminalNo, string RRN, string HasCardNumber, string Amount)
        {
            var cookies = HttpContext.Request.Cookies;
            string cookiBaseket = cookies.Where(a => a.Key == "ProgrammingCalssShoppingcart").SingleOrDefault().Value;
            if (cookiBaseket == null)
                return View();

            var shoppingcart = _context.TblShoppingcart.Where(
                a => a.Cookie == cookiBaseket
                && !a.IsPaied
                && a.IsSentToBank)
                .Include(a => a.TblShoppingCartDetails)
                .Include(a => a.TblUserAddress)
                .SingleOrDefault();

            if (shoppingcart == null)
            {
                TempData["W"] = ErrMsg.IncorrectInformation;
                return RedirectToAction(nameof(Index));
            }
            shoppingcart.IsSentToBank = false;
            _context.Update(shoppingcart);
            await _context.SaveChangesAsync();

            var checkShopping = _context.TblShoppingcart.Where(a => a.CustomOrderNumber == OrderId).SingleOrDefault();
            if (checkShopping == null)
            {
                TempData["W"] = ErrMsg.IncorrectInformation;
                return RedirectToAction(nameof(Index));
            }
            if (checkShopping.Id != shoppingcart.Id)
            {
                TempData["W"] = ErrMsg.IncorrectInformation;
                return RedirectToAction(nameof(Index));
            }

            string errors = null;

            switch (Convert.ToInt32(status))
            {
                case -32768:
                    errors = " خطاي ناشناخته رخ داده است";
                    break;

                case -1552:
                    errors = "برگشت تراکنش مجاز نمی باشد";
                    break;
                case -1551:
                    errors = "برگشت تراکنش قبلا اًنجام شده است ";
                    break;
                case -1550:
                    errors = "  برگشت تراکنش در وضعیت جاري امکان پذیر نمی باشد ";
                    break;
                case -1549:
                    errors = "زمان مجاز براي درخواست برگشت تراکنش به اتمام رسیده است ";
                    break;

                case -1548:
                    errors = "فراخوانی سرویس درخواست پرداخت قبض  ناموفق بود";
                    break;
                case -1540:
                    errors = "تایید تراکنش ناموفق می باشد ";
                    break;
                case -1536:
                    errors = "فراخوانی سرویس درخواست شارژ تاپ آپ ناموفق بود";
                    break;
                case -1533:
                    errors = "تراکنش قبلاً تایید شده است";
                    break;

                case -1532:
                    errors = "تراکنش از سوي پذیرنده تایید شد";
                    break;
                case -1531:
                    errors = "تایید تراکنش ناموفق امکان پذیر نمی باشد  ";
                    break;
                case -1530:
                    errors = "پذیرنده مجاز به تایید این تراکنش نمی باشد";
                    break;

                case -1528:
                    errors = "اطلاعات پرداخت یافت نشد";
                    break;
                case -1527:
                    errors = "انجام عملیات درخواست پرداخت تراکنش خرید ناموفق بود";
                    break;

                case -1507:
                    errors = "تراکنش برگشت به سوئیچ ارسال شد";
                    break;

                case -1505:
                    errors = "تایید تراکنش توسط پذیرنده انجام شد ";
                    break;

                case -138:
                    errors = "عملیات پرداخت توسط کاربر لغو شد ";
                    break;

                case -132:
                    errors = "مبلغ تراکنش کمتر از حداقل مجاز میباشد  ";
                    break;
                case -131:
                    errors = "نامعتبر می باشد Token  ";
                    break;
                case -130:
                    errors = "زمان منقضی شده است Token";
                    break;
                case -128:
                    errors = "معتبر نمی باشد IP قالب آدرس";
                    break;

                case -127:
                    errors = "آدرس اینترنتی معتبر نمی باشد";
                    break;
                case -126:
                    errors = "کد شناسایی پذیرنده معتبر نمی باشد  ";
                    break;
                case -121:
                    errors = "رشته داده شده بطور کامل عددي نمی باشد ";
                    break;
                case -120:
                    errors = "طول داده ورودي معتبر نمی باشد";
                    break;
                case -119:
                    errors = "سازمان نامعتبر می باشد ";
                    break;
                case -118:
                    errors = "مقدار ارسال شده عدد نمی باشد";
                    break;

                case -117:
                    errors = "طول رشته کم تر از حد مجاز می باشد";
                    break;

                case -116:
                    errors = "طول رشته بیش از حد مجاز می باشد";
                    break;
                case -115:
                    errors = "شناسه پرداخت نامعتبر می باشد";
                    break;

                case -114:
                    errors = "شناسه قبض نامعتبر می باشد";
                    break;

                case -113:
                    errors = "پارامتر ورودي خالی می باشد ";
                    break;
                case -112:
                    errors = "شماره سفارش تکراري است ";
                    break;

                case -111:
                    errors = "مبلغ تراکنش بیش از حد مجاز پذیرنده می باشد";
                    break;
                case -108:
                    errors = "قابلیت برگشت تراکنش براي پذیرنده غیر فعال می باشد";
                    break;
                case -107:
                    errors = " قابلیت ارسال تاییده تراکنش براي پذیرنده غیر فعال می باشد";
                    break;
                case -106:
                    errors = "قابلیت شارژ براي پذیرنده غیر فعال می باشد";
                    break;
                case -105:
                    errors = "قابلیت تاپ آپ براي پذیرنده غیر فعال می باشد";
                    break;
                case -104:
                    errors = "قابلیت پرداخت قبض براي پذیرنده غیر فعال می باشد";
                    break;
                case -103:
                    errors = "قابلیت خرید براي پذیرنده غیر فعال می باشد ";
                    break;
                case -102:
                    errors = "تراکنش با موفقیت برگشت داده شد ";
                    break;
                case -101:
                    errors = "پذیرنده اهراز هویت نشد ";
                    break;
                case -100:
                    errors = "پذیرنده غیرفعال می باشد";
                    break;
                case -1:
                    errors = "خطاي سرور Server Error ";
                    break;
                case 1:
                    errors = "صادرکننده ي کارت از انجام تراکنش صرف نظر کرد";
                    break;
                case 2:
                    errors = "عملیات تاییدیه این تراکنش قبلا باموفقیت  صورت پذیرفته است";
                    break;
                case 3:
                    errors = "پذیرنده ي فروشگاهی نامعتبر می باشد ";
                    break;
                case 5:
                    errors = "از انجام تراکنش صرف نظر شد ";
                    break;
                case 6:
                    errors = "بروز خطایی ناشناخته ";
                    break;

                case 8:
                    errors = "باتشخیص هویت دارنده ي کارت، تراکنش موفق می باشد";
                    break;
                case 9:
                    errors = "درخواست رسیده در حال پی گیري و انجام است";
                    break;
                case 10:
                    errors = "تراکنش با مبلغی پایین تر از مبلغ درخواستی  )کمبود حساب مشتري(پذیرفته شده است";
                    break;
                case 12:
                    errors = "تراکنش نامعتبر است";
                    break;
                case 13:
                    errors = "مبلغ تراکنش نادرست است";
                    break;
                case 14:
                    errors = "شماره کارت ارسالی نامعتبر است(وجود ندارد)";
                    break;
                case 15:
                    errors = "صادرکننده ي کارت نامعتبراست(وجود ندارد)";
                    break;

                case 17:
                    errors = "مشتري درخواست کننده حذف شده است Customer Cancellation ";
                    break;

                case 20:
                    errors = "در موقعیتی که سوئیچ جهت پذیرش تراکنش نیازمند پرس و جو از کارت است ممکن است درخواست از کارت(ترمینال) بنماید این پیام مبین نامعتبر بودن جواب است";
                    break;
                case 21:
                    errors = "خطا در ارسال به درگاه پرداخت: کد خطا 21";
                    break;
                case 22:
                    errors = "تراکنش مشکوك به بد عمل کردن(کارت، ترمینال ، دارنده کارت) بوده است لذا پذیرفته نشده است";
                    break;
                case 30:
                    errors = "قالب پیام داراي اشکال است  ";
                    break;
                case 31:
                    errors = "پذیرنده توسط سوئی پشتیبانی نمی شود  ";
                    break;
                case 32:
                    errors = "تراکنش به صورت غیر قطعی کامل شده است Completed Partially ";
                    break;
                case 33:
                    errors = "تاریخ انقضاي کارت سپري شده است  ";
                    break;
                case 38:
                    errors = "تعداد دفعات ورود رمزغلط بیش از حدمجاز است";
                    break;
                case 39:
                    errors = "کارت حساب اعتباري ندارد";
                    break;

                case 40:
                    errors = "عملیات درخواستی پشتیبانی نمی گردد ";
                    break;
                case 41:
                    errors = "کارت مفقودي می باشد  ";
                    break;
                case 43:
                    errors = "کارت مسروقه می باشد  ";
                    break;
                case 45:
                    errors = "قبض قابل پرداخت نمی باشد ";
                    break;
                case 51:
                    errors = "موجودي کافی نمی باشد ";
                    break;
                case 54:
                    errors = "تاریخ انقضاي کارت سپري شده است ";
                    break;
                case 55:
                    errors = "رمز کارت نا معتبر است";
                    break;
                case 56:
                    errors = "کارت نا معتبر است ";
                    break;
                case 57:
                    errors = "انجام تراکنش مربوطه توسط دارنده ي  کارت مجاز نمی باشد";
                    break;
                case 58:
                    errors = "انجام تراکنش مربوطه توسط پایانه ي انجام دهنده مجاز نمی باشد";
                    break;
                case 59:
                    errors = "کارت مظنون به تقلب است ";
                    break;
                case 61:
                    errors = "مبلغ تراکنش بیش از حد مجاز می باشد ";
                    break;
                case 62:
                    errors = "کارت محدود شده است ";
                    break;
                case 63:
                    errors = "تمهیدات امنیتی نقضگردیده است  ";
                    break;
                case 65:
                    errors = "تعداد درخواست تراکنش بیش از حد مجاز می باشد";
                    break;
                case 68:
                    errors = "پاسخ لازم براي تکمیل یا انجام تراکنش خیلی دیر رسیده است";
                    break;
                case 69:
                    errors = "تعداد دفعات تکرار رمز از حد مجاز گذشته است";
                    break;
                case 75:
                    errors = "تعداد دفعات ورود رمزغلط بیش از حدمجاز است";
                    break;
                case 78:
                    errors = "کارت فعال نیست ";
                    break;
                case 79:
                    errors = "حساب متصل به کارت نا معتبر است یا داراي اشکال است";
                    break;
                case 80:
                    errors = "درخواست تراکنش رد شده است ";
                    break;
                case 81:
                    errors = "کارت پذیرفته نشد ";
                    break;
                case 83:
                    errors = "سرویس دهنده سوئیچ کارت تراکنش را نپذیرفته است";
                    break;
                case 84:
                    errors = " Issuer Down-Slm";
                    break;
                case 91:
                    errors = "سیستم صدور مجوز انجام تراکنش موقتا غیر  فعال است و یا زمان تعیین شده براي صدو مجوز به پایان رسیده است";
                    break;
                case 92:
                    errors = " مقصد تراکنش پیدا نشد";
                    break;
                case 93:
                    errors = "امکان تکمیل تراکنش وجود ندارد ";
                    break;
            }
            if (errors != null)
            {
                TempData["W"] = errors;
                return RedirectToAction(nameof(Index));
            }
            if (status != "0")
            {
                TempData["W"] = "خطای نا شناخته در پرداخت!!! لطفا مجددا اقدام نمایید";
                return RedirectToAction(nameof(Index));
            }
            //وقتی از کدهای بالا عبور کردیم به این معناست که میتوانیم خرید را نهایی کنیم و تاییده را به درگاه بانک ارسال نماییم

            ClientConfirmRequestData clientConfirmRequestData = new ClientConfirmRequestData()
            {
                LoginAccount = "231231231",
                Token = Token
            };

            ConfirmServiceSoapClient confirmServiceSoapClient = new ConfirmServiceSoapClient(ConfirmServiceSoapClient.EndpointConfiguration.ConfirmServiceSoap);
            ConfirmPaymentRequestBody confirmPaymentRequestBody = new ConfirmPaymentRequestBody(clientConfirmRequestData);

            ConfirmPaymentRequest confirmPaymentRequest = new ConfirmPaymentRequest(confirmPaymentRequestBody);

            var verifyPay = await confirmServiceSoapClient.ConfirmPaymentAsync(clientConfirmRequestData);


            if (verifyPay.Body.ConfirmPaymentResult.Status != 0)
            {
                switch (Convert.ToInt32(verifyPay.Body.ConfirmPaymentResult.Status))
                {
                    case -32768:
                        errors = " خطاي ناشناخته رخ داده است";
                        break;

                    case -1552:
                        errors = "برگشت تراکنش مجاز نمی باشد";
                        break;
                    case -1551:
                        errors = "برگشت تراکنش قبلا اًنجام شده است ";
                        break;
                    case -1550:
                        errors = "  برگشت تراکنش در وضعیت جاري امکان پذیر نمی باشد ";
                        break;
                    case -1549:
                        errors = "زمان مجاز براي درخواست برگشت تراکنش به اتمام رسیده است ";
                        break;

                    case -1548:
                        errors = "فراخوانی سرویس درخواست پرداخت قبض  ناموفق بود";
                        break;
                    case -1540:
                        errors = "تایید تراکنش ناموفق می باشد ";
                        break;
                    case -1536:
                        errors = "فراخوانی سرویس درخواست شارژ تاپ آپ ناموفق بود";
                        break;
                    case -1533:
                        errors = "تراکنش قبلاً تایید شده است";
                        break;

                    case -1532:
                        errors = "تراکنش از سوي پذیرنده تایید شد";
                        break;
                    case -1531:
                        errors = "تایید تراکنش ناموفق امکان پذیر نمی باشد  ";
                        break;
                    case -1530:
                        errors = "پذیرنده مجاز به تایید این تراکنش نمی باشد";
                        break;

                    case -1528:
                        errors = "اطلاعات پرداخت یافت نشد";
                        break;
                    case -1527:
                        errors = "انجام عملیات درخواست پرداخت تراکنش خرید ناموفق بود";
                        break;

                    case -1507:
                        errors = "تراکنش برگشت به سوئیچ ارسال شد";
                        break;

                    case -1505:
                        errors = "تایید تراکنش توسط پذیرنده انجام شد ";
                        break;

                    case -138:
                        errors = "عملیات پرداخت توسط کاربر لغو شد ";
                        break;

                    case -132:
                        errors = "مبلغ تراکنش کمتر از حداقل مجاز میباشد  ";
                        break;
                    case -131:
                        errors = "نامعتبر می باشد Token  ";
                        break;
                    case -130:
                        errors = "زمان منقضی شده است Token";
                        break;
                    case -128:
                        errors = "معتبر نمی باشد IP قالب آدرس";
                        break;

                    case -127:
                        errors = "آدرس اینترنتی معتبر نمی باشد";
                        break;
                    case -126:
                        errors = "کد شناسایی پذیرنده معتبر نمی باشد  ";
                        break;
                    case -121:
                        errors = "رشته داده شده بطور کامل عددي نمی باشد ";
                        break;
                    case -120:
                        errors = "طول داده ورودي معتبر نمی باشد";
                        break;
                    case -119:
                        errors = "سازمان نامعتبر می باشد ";
                        break;
                    case -118:
                        errors = "مقدار ارسال شده عدد نمی باشد";
                        break;

                    case -117:
                        errors = "طول رشته کم تر از حد مجاز می باشد";
                        break;

                    case -116:
                        errors = "طول رشته بیش از حد مجاز می باشد";
                        break;
                    case -115:
                        errors = "شناسه پرداخت نامعتبر می باشد";
                        break;

                    case -114:
                        errors = "شناسه قبض نامعتبر می باشد";
                        break;

                    case -113:
                        errors = "پارامتر ورودي خالی می باشد ";
                        break;
                    case -112:
                        errors = "شماره سفارش تکراري است ";
                        break;

                    case -111:
                        errors = "مبلغ تراکنش بیش از حد مجاز پذیرنده می باشد";
                        break;
                    case -108:
                        errors = "قابلیت برگشت تراکنش براي پذیرنده غیر فعال می باشد";
                        break;
                    case -107:
                        errors = " قابلیت ارسال تاییده تراکنش براي پذیرنده غیر فعال می باشد";
                        break;
                    case -106:
                        errors = "قابلیت شارژ براي پذیرنده غیر فعال می باشد";
                        break;
                    case -105:
                        errors = "قابلیت تاپ آپ براي پذیرنده غیر فعال می باشد";
                        break;
                    case -104:
                        errors = "قابلیت پرداخت قبض براي پذیرنده غیر فعال می باشد";
                        break;
                    case -103:
                        errors = "قابلیت خرید براي پذیرنده غیر فعال می باشد ";
                        break;
                    case -102:
                        errors = "تراکنش با موفقیت برگشت داده شد ";
                        break;
                    case -101:
                        errors = "پذیرنده اهراز هویت نشد ";
                        break;
                    case -100:
                        errors = "پذیرنده غیرفعال می باشد";
                        break;
                    case -1:
                        errors = "خطاي سرور Server Error ";
                        break;
                    case 1:
                        errors = "صادرکننده ي کارت از انجام تراکنش صرف نظر کرد";
                        break;
                    case 2:
                        errors = "عملیات تاییدیه این تراکنش قبلا باموفقیت  صورت پذیرفته است";
                        break;
                    case 3:
                        errors = "پذیرنده ي فروشگاهی نامعتبر می باشد ";
                        break;
                    case 5:
                        errors = "از انجام تراکنش صرف نظر شد ";
                        break;
                    case 6:
                        errors = "بروز خطایی ناشناخته ";
                        break;

                    case 8:
                        errors = "باتشخیص هویت دارنده ي کارت، تراکنش موفق می باشد";
                        break;
                    case 9:
                        errors = "درخواست رسیده در حال پی گیري و انجام است";
                        break;
                    case 10:
                        errors = "تراکنش با مبلغی پایین تر از مبلغ درخواستی  )کمبود حساب مشتري(پذیرفته شده است";
                        break;
                    case 12:
                        errors = "تراکنش نامعتبر است";
                        break;
                    case 13:
                        errors = "مبلغ تراکنش نادرست است";
                        break;
                    case 14:
                        errors = "شماره کارت ارسالی نامعتبر است(وجود ندارد)";
                        break;
                    case 15:
                        errors = "صادرکننده ي کارت نامعتبراست(وجود ندارد)";
                        break;

                    case 17:
                        errors = "مشتري درخواست کننده حذف شده است Customer Cancellation ";
                        break;

                    case 20:
                        errors = "در موقعیتی که سوئیچ جهت پذیرش تراکنش نیازمند پرس و جو از کارت است ممکن است درخواست از کارت(ترمینال) بنماید این پیام مبین نامعتبر بودن جواب است";
                        break;
                    case 21:
                        errors = "خطا در ارسال به درگاه پرداخت: کد خطا 21";
                        break;
                    case 22:
                        errors = "تراکنش مشکوك به بد عمل کردن(کارت، ترمینال ، دارنده کارت) بوده است لذا پذیرفته نشده است";
                        break;
                    case 30:
                        errors = "قالب پیام داراي اشکال است  ";
                        break;
                    case 31:
                        errors = "پذیرنده توسط سوئی پشتیبانی نمی شود  ";
                        break;
                    case 32:
                        errors = "تراکنش به صورت غیر قطعی کامل شده است Completed Partially ";
                        break;
                    case 33:
                        errors = "تاریخ انقضاي کارت سپري شده است  ";
                        break;
                    case 38:
                        errors = "تعداد دفعات ورود رمزغلط بیش از حدمجاز است";
                        break;
                    case 39:
                        errors = "کارت حساب اعتباري ندارد";
                        break;

                    case 40:
                        errors = "عملیات درخواستی پشتیبانی نمی گردد ";
                        break;
                    case 41:
                        errors = "کارت مفقودي می باشد  ";
                        break;
                    case 43:
                        errors = "کارت مسروقه می باشد  ";
                        break;
                    case 45:
                        errors = "قبض قابل پرداخت نمی باشد ";
                        break;
                    case 51:
                        errors = "موجودي کافی نمی باشد ";
                        break;
                    case 54:
                        errors = "تاریخ انقضاي کارت سپري شده است ";
                        break;
                    case 55:
                        errors = "رمز کارت نا معتبر است";
                        break;
                    case 56:
                        errors = "کارت نا معتبر است ";
                        break;
                    case 57:
                        errors = "انجام تراکنش مربوطه توسط دارنده ي  کارت مجاز نمی باشد";
                        break;
                    case 58:
                        errors = "انجام تراکنش مربوطه توسط پایانه ي انجام دهنده مجاز نمی باشد";
                        break;
                    case 59:
                        errors = "کارت مظنون به تقلب است ";
                        break;
                    case 61:
                        errors = "مبلغ تراکنش بیش از حد مجاز می باشد ";
                        break;
                    case 62:
                        errors = "کارت محدود شده است ";
                        break;
                    case 63:
                        errors = "تمهیدات امنیتی نقضگردیده است  ";
                        break;
                    case 65:
                        errors = "تعداد درخواست تراکنش بیش از حد مجاز می باشد";
                        break;
                    case 68:
                        errors = "پاسخ لازم براي تکمیل یا انجام تراکنش خیلی دیر رسیده است";
                        break;
                    case 69:
                        errors = "تعداد دفعات تکرار رمز از حد مجاز گذشته است";
                        break;
                    case 75:
                        errors = "تعداد دفعات ورود رمزغلط بیش از حدمجاز است";
                        break;
                    case 78:
                        errors = "کارت فعال نیست ";
                        break;
                    case 79:
                        errors = "حساب متصل به کارت نا معتبر است یا داراي اشکال است";
                        break;
                    case 80:
                        errors = "درخواست تراکنش رد شده است ";
                        break;
                    case 81:
                        errors = "کارت پذیرفته نشد ";
                        break;
                    case 83:
                        errors = "سرویس دهنده سوئیچ کارت تراکنش را نپذیرفته است";
                        break;
                    case 84:
                        errors = " Issuer Down-Slm";
                        break;
                    case 91:
                        errors = "سیستم صدور مجوز انجام تراکنش موقتا غیر  فعال است و یا زمان تعیین شده براي صدو مجوز به پایان رسیده است";
                        break;
                    case 92:
                        errors = " مقصد تراکنش پیدا نشد";
                        break;
                    case 93:
                        errors = "امکان تکمیل تراکنش وجود ندارد ";
                        break;
                }
            }

            if (errors != null)
            {
                TempData["W"] = errors;
                return RedirectToAction(nameof(Index));
            }
            //در این قسمت اگر تمامی موارد بالا اوکی باشد به این معناست که پرداخت تمام شده و تاییدیه بانک هم موفقیت آمیز بوده است 

            shoppingcart.IsSentToBank = true;
            shoppingcart.IsPaied = true;
            shoppingcart.PayDate = DateTime.Now;
            shoppingcart.TransactionNumber = verifyPay.Body.ConfirmPaymentResult.RRN.ToString();
            _context.Update(shoppingcart);
            await _context.SaveChangesAsync();

            foreach (var item in shoppingcart.TblShoppingCartDetails)
            {
                var product = _context.Tblproduct.Find(item.ProductId);
                product.SoldCount += item.Count;
                product.StockQuantity -= item.Count;
                _context.Update(product);
                await _context.SaveChangesAsync();
            }
            Response.Cookies.Delete("ProgrammingCalssShoppingcart");

            ///در این قسمت میتوانید کد تراکنش را به کاربر اس ام اس کنید    

            TempData["S"] = "عملیات خرید با موفقیت انجام گرفت";
            return RedirectToAction(nameof(Index));
        }



    }
}
