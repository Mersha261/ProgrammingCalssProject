using PgrogrammingClass.Core.Domain;

namespace ProgramingCalssProject.Models.ViewModel
{
    public class IndexModeldata
    {
        public List<TblBanner> Banners { get; set; }
        public List<Tblproduct> NewProduct { get; set; }
        public List<Tblproduct> MostSoldProduct { get; set; }
        public List<Tblproduct> MostViewdProduct { get; set; }

        public string IndexSentens { get; set; }
    }
}
