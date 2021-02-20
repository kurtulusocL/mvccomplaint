using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Complain.Web.Toolkits
{
    public static class Tiempo
    {
        public static string ElepsedTime(this DateTime date)
        {
            var timeSpan = DateTime.Now - date;

            if (timeSpan <= TimeSpan.FromSeconds(60))
                return string.Format("{0} saniye önce", timeSpan.Seconds);

            else if (timeSpan <= TimeSpan.FromMinutes(60))
                return timeSpan.Minutes > 1 ? string.Format("{0} dakika önce", timeSpan.Minutes) : "yaklaşık bir dakika önce";

            else if (timeSpan <= TimeSpan.FromHours(24))
                return timeSpan.Hours > 1 ? String.Format("{0} saat önce", timeSpan.Hours) : "yaklaşık bir saat önce";

            else if (timeSpan <= TimeSpan.FromDays(30))
                return timeSpan.Days > 1 ? String.Format("{0} gün önce", timeSpan.Days) : "dün";

            else if (timeSpan <= TimeSpan.FromDays(365))
                return timeSpan.Days > 30 ? String.Format("{0} ay önce", timeSpan.Days / 30) : "yaklaşık bir ay önce";

            return timeSpan.Days > 365 ? String.Format("{0} yıl önce", timeSpan.Days / 365) : "yaklaşık bir yıl önce";
        }
    }
}