using Complain.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Complain.Web.SEOLink
{
    public static class AboutSeo
    {
        public static string aboutseo(this UrlHelper urlHelper, About entity)
        {
            string title = entity.Title;
            title = kurtulusURLTitle(title);
            return string.Format("/Detail/{0}/{1}", entity.Id.ToString(), title);
        }

        private static string kurtulusURLTitle(string pTitle)
        {
            pTitle = pTitle.Replace(" ", "-");
            pTitle = pTitle.Replace(".", "-");
            pTitle = pTitle.Replace("ı", "i");
            pTitle = pTitle.Replace("İ", "I");

            pTitle = String.Join("", pTitle.Normalize(NormalizationForm.FormD) // türkçe karakterleri ingilizceye çevir.
                    .Where(c => char.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark));

            pTitle = HttpUtility.UrlEncode(pTitle);
            return System.Text.RegularExpressions.Regex.Replace(pTitle, @"\%[0-9A-Fa-f]{2}", "");
        }
    }
}