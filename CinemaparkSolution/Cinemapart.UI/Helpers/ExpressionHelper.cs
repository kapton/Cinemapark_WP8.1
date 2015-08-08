using System;
using System.Xml.Linq;

namespace Cinemapart.UI.Helpers
{
    public static class ExpressionHelper
    {
        public static string GetAttributeOrDefault(this XElement element, string attributeName)
        {
            var attr = element.Attribute(attributeName);
            return attr != null ? attr.Value : string.Empty;
        }

        public static int GetAttributeIntOrDefault(this XElement element, string attributeName)
        {
            var attr = element.Attribute(attributeName);
            if (attr != null)
            {
                int res;
                if (int.TryParse(attr.Value, out res))
                    return res;
            }
            return 0;
        }

        public static DateTime GetTime(this string date, string time)
        {
            var dt = date.Split(',')[0].Trim();
            var day = Convert.ToInt32(dt.Split()[0].Trim());
            var m = 1;
            switch (dt.Split()[1].ToLower())
            {
                case "января": m = 1; break;
                case "февраля": m = 2; break;
                case "марта": m = 3; break;
                case "апреля": m = 4; break;
                case "мая": m = 5; break;
                case "июня": m = 6; break;
                case "июля": m = 7; break;
                case "августа": m = 8; break;
                case "сентября": m = 9; break;
                case "октября": m = 10; break;
                case "ноября": m = 11; break;
                case "декабря": m = 12; break;
                default: m = DateTime.Today.Month; break;
            }
            var year = m != DateTime.Today.Month ? DateTime.Today.Year + 1 : DateTime.Today.Year;
            var hour = Convert.ToInt32(time.Split(':')[0]);
            var minute = Convert.ToInt32(time.Split(':')[1]);

            return new DateTime(year, m, day, hour, minute, 0);
        }
    }
}
