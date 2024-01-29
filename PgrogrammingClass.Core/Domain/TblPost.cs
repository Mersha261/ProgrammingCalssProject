using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PgrogrammingClass.Core.Domain
{
    public class TblPost:BaseEntity
    {
        [Display(Name = "از 0 تا 500 پیشتاز درون استانی")]
        public int InZeroTooFiveHunderedPishtaz { get; set; }


        [Display(Name = "از 0 تا 500 سفارشی درون استانی")]
        public int InZeroTooFiveHunderedSefareshi { get; set; }



        [Display(Name = "از 500 تا 1000 پیشتاز درون استانی")]
        public int InFiveHondredToThousandPishtaz { get; set; }

        [Display(Name = "از 500 تا 1000 سفارشی درون استانی")]
        public int InFiveHondredToThousandSefareshi { get; set; }


        [Display(Name = "از 1000 تا 2000 پیشتاز درون استانی")]
        public int InOnetoTowThousandPishtaz { get; set; }


        [Display(Name = "از 1000 تا 2000 سفارشی درون استانی")]
        public int InOnetoTowThousandSefareshi { get; set; }


        [Display(Name = "از 2000 تا 5000 سفارشی درون استانی")]
        public int InTowtoFiveThousandSefareshi { get; set; }



        [Display(Name = "از 2000 تا 3000 پیشتاز درون استانی")]
        public int InTowtoThreeThousandPishtaz { get; set; }



        [Display(Name = "از 3000 تا 4000 پیشتاز درون استانی")]
        public int InThreetoFourThousandPishtaz { get; set; }


        [Display(Name = "از 4000 تا 5000 پیشتاز درون استانی")]
        public int InFourtoFiveThousandPishtaz { get; set; }



        [Display(Name = "مبلغ در ازای هر کیلو بیشتر سفارشی درون استانی")]
        public int InPerKiloSefareshi { get; set; }

        [Display(Name = "مبلغ در ازای هر کیلو بیشتر پیشتاز درون استانی")]
        public int InPerKiloPishtaz { get; set; }




        [Display(Name = "از 0 تا 500 پیشتاز بیرون استانی")]
        public int OutZeroTooFiveHunderedPishtaz { get; set; }


        [Display(Name = "از 0 تا 500 سفارشی بیرون استانی")]
        public int OutZeroTooFiveHunderedSefareshi { get; set; }



        [Display(Name = "از 500 تا 1000 پیشتاز بیرون استانی")]
        public int OutFiveHondredToThousandPishtaz { get; set; }

        [Display(Name = "از 500 تا 1000 سفارشی بیرون استانی")]
        public int OutFiveHondredToThousandSefareshi { get; set; }


        [Display(Name = "از 1000 تا 2000 پیشتاز بیرون استانی")]
        public int OutOnetoTowThousandPishtaz { get; set; }


        [Display(Name = "از 1000 تا 2000 سفارشی بیرون استانی")]
        public int OutOnetoTowThousandSefareshi { get; set; }


        [Display(Name = "از 2000 تا 5000 سفارشی بیرون استانی")]
        public int OutTowtoFiveThousandSefareshi { get; set; }



        [Display(Name = "از 2000 تا 3000 پیشتاز بیرون استانی")]
        public int OutTowtoThreeThousandPishtaz { get; set; }



        [Display(Name = "از 3000 تا 4000 پیشتاز بیرون استانی")]
        public int OutThreetoFourThousandPishtaz { get; set; }


        [Display(Name = "از 4000 تا 5000 پیشتاز بیرون استانی")]
        public int OutFourtoFiveThousandPishtaz { get; set; }



        [Display(Name = "مبلغ در ازای هر کیلو بیشتر سفارشی بیرون استانی")]
        public int OutPerKiloSefareshi { get; set; }

        [Display(Name = "مبلغ در ازای هر کیلو بیشتر پیشتاز بیرون استانی")]
        public int OutPerKiloPishtaz { get; set; }
    }
}
