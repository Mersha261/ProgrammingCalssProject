using Microsoft.EntityFrameworkCore;
using PgrogrammingClass.Core.Domain;
using PgrogrammingClass.Data.DataContext;
using PgrogrammingClass.Data.Migrations;
using ProgramingCalssProject.Models.ViewModel;
using System.Net;

namespace ProgramingCalssProject.Models.Getdata
{
    public interface IGetData
    {
        public List<TblBanner> GetBanner();
        public List<Tblproduct> GetMostSold();
        public string GetImageByProductId(int productId);

        public List<TblUserAddress> getUserAddressByUserId(string id);

        public TblUserAddress GetAddreeeById(int id);

        public List<AddShippingViewModel> GetShippingWay(int Province, int Weight);


        public List<TblCategory> GetCategoryParrent();
        public List<TblCategory> GetCategoryByParrentId(int id);
    }

    public class GetData : IGetData
    {
        private readonly ApplicationDbContext _context;

        public GetData(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<TblBanner> GetBanner()
        {
            return _context.TblBanner.OrderByDescending(a => a.Id).ToList();
        }

        public List<Tblproduct> GetMostSold()
        {
            return _context.Tblproduct.Where(a => !a.IsDeleted).Include(a => a.ProductImages).OrderBy(a => a.SoldCount).ToList();
        }
        public string GetImageByProductId(int productId)
        {
            return _context.TblProductImage.Where(a => a.ProductId == productId).FirstOrDefault().Thumbnaile;
        }

        public List<TblUserAddress> getUserAddressByUserId(string id)
        {
            return _context.TblUserAddress.Where(a => a.UserId == id).Include(a => a.TblCity).ThenInclude(a => a.TblProvince).ToList();
        }

        public TblUserAddress GetAddreeeById(int id)
        {
            return _context.TblUserAddress.Where(a => a.Id == id).Include(a => a.TblCity).ThenInclude(a => a.TblProvince).SingleOrDefault();
        }

        public List<AddShippingViewModel> GetShippingWay(int Province, int Weight)
        {
            int Sefareshi = 0;
            int Pishtaz = 0;

            var tblpost = _context.TblPost.FirstOrDefault();

            int myProvince = _context.TblCity.Where(a => a.CityName == "شیراز").SingleOrDefault().ProvinceId;

            if (myProvince == Province)
            {
                if (Weight > 0 && Weight <= 500)
                {
                    Sefareshi = tblpost.InZeroTooFiveHunderedSefareshi;
                    Pishtaz = tblpost.InZeroTooFiveHunderedPishtaz;
                }
                if (Weight > 500 && Weight <= 1000)
                {
                    Sefareshi = tblpost.InFiveHondredToThousandSefareshi;
                    Pishtaz = tblpost.InFiveHondredToThousandPishtaz;
                }

                if (Weight > 1000 && Weight <= 2000)
                {
                    Sefareshi = tblpost.InOnetoTowThousandSefareshi;
                    Pishtaz = tblpost.InOnetoTowThousandPishtaz;
                }

                if (Weight > 2000 && Weight <= 5000)
                {
                    Sefareshi = tblpost.InTowtoFiveThousandSefareshi;

                    if (Weight > 2000 && Weight <= 3000)
                    {
                        Pishtaz = tblpost.InTowtoThreeThousandPishtaz;
                    }
                    if (Weight > 3000 && Weight <= 4000)
                    {
                        Pishtaz = tblpost.InThreetoFourThousandPishtaz;
                    }
                    if (Weight > 3000 && Weight <= 4000)
                    {
                        Pishtaz = tblpost.InFourtoFiveThousandPishtaz;
                    }
                }
                if (Weight > 5000)
                {
                    Weight -= 5000;
                    Weight /= 1000;
                    Weight += 1;

                    Sefareshi = Weight * tblpost.InPerKiloSefareshi;
                    Sefareshi += tblpost.InTowtoFiveThousandSefareshi;

                    Pishtaz = Weight * tblpost.InPerKiloPishtaz;
                    Pishtaz += tblpost.InFourtoFiveThousandPishtaz;
                }

            }
            else
            {
                if (Weight > 0 && Weight <= 500)
                {
                    Sefareshi = tblpost.OutZeroTooFiveHunderedSefareshi;
                    Pishtaz = tblpost.OutZeroTooFiveHunderedPishtaz;
                }
                if (Weight > 500 && Weight <= 1000)
                {
                    Sefareshi = tblpost.OutFiveHondredToThousandSefareshi;
                    Pishtaz = tblpost.OutFiveHondredToThousandPishtaz;
                }

                if (Weight > 1000 && Weight <= 2000)
                {
                    Sefareshi = tblpost.OutOnetoTowThousandSefareshi;
                    Pishtaz = tblpost.OutOnetoTowThousandPishtaz;
                }

                if (Weight > 2000 && Weight <= 5000)
                {
                    Sefareshi = tblpost.OutTowtoFiveThousandSefareshi;

                    if (Weight > 2000 && Weight <= 3000)
                    {
                        Pishtaz = tblpost.OutTowtoThreeThousandPishtaz;
                    }
                    if (Weight > 3000 && Weight <= 4000)
                    {
                        Pishtaz = tblpost.OutThreetoFourThousandPishtaz;
                    }
                    if (Weight > 3000 && Weight <= 4000)
                    {
                        Pishtaz = tblpost.OutFourtoFiveThousandPishtaz;
                    }
                }
                if (Weight > 5000)
                {
                    Weight -= 5000;
                    Weight /= 1000;
                    Weight += 1;

                    Sefareshi = Weight * tblpost.OutPerKiloSefareshi;
                    Sefareshi += tblpost.OutTowtoFiveThousandSefareshi;

                    Pishtaz = Weight * tblpost.OutPerKiloPishtaz;
                    Pishtaz += tblpost.OutFourtoFiveThousandPishtaz;
                }
            }

            List<AddShippingViewModel> addShippingViewModels = new List<AddShippingViewModel>()
            {
                new AddShippingViewModel()
                {
                    ShippingName="پست سفارشی",
                    ShippingPrice=Sefareshi
                },
                new AddShippingViewModel()
                {
                    ShippingName="پست پیشتاز",
                    ShippingPrice=Pishtaz
                }
                ,
                new AddShippingViewModel()
                {
                    ShippingName="ارسال و پرداخت درب منزل",
                    ShippingPrice=0
                }
            };

            return addShippingViewModels;

        }

        public List<TblCategory> GetCategoryParrent()
        {
            return _context.TblCategory.Where(a => a.IsIncludeInTopMenu && a.Name != "همه").ToList();
        }

        public List<TblCategory> GetCategoryByParrentId(int id)
        {
            return _context.TblCategory.Where(a=>a.ParentCategoryId == id).ToList();
        }
    }
}
