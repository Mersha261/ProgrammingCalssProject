using System.Globalization;

namespace ProgramingCalssProject.Models.Utillity
{
    public interface IpersianDateTime
    {
        public string GetPersinaDate(DateTime dt);

        public DateTime Gregorian(int year, int month, int day, int hour, int minute);
    }

    public class PersianDateTime : IpersianDateTime
    {
        public string GetPersinaDate(DateTime dt)
        {
            if (dt < DateTime.Now.AddYears(-1000))
            {
                return null;
            }

            PersianCalendar pc = new PersianCalendar();
            int year = pc.GetYear(dt);
            int month = pc.GetMonth(dt);
            int day = pc.GetDayOfMonth(dt);
            int hour = pc.GetHour(dt);
            int minute = pc.GetMinute(dt);

            return (year + "/" + month + "/" + day + " ساعت " + hour + ":" + minute);
        }

        public DateTime Gregorian(int year, int month, int day, int hour, int minute)
        {
            PersianCalendar pc = new PersianCalendar();
            DateTime dt = new DateTime(year, month, day, hour, minute,0,pc);
            return dt;
        }
    }
}
