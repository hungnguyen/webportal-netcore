using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPortal.Services.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Compare current <see cref="DateTimeOffset"/> with Now
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static int CompareToNow(this DateTimeOffset? d) =>
            d.CompareTo(DateTimeOffset.Now);

        /// <summary>
        /// Compare current <see cref="DateTimeOffset"/> with other
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static int CompareTo(this DateTimeOffset? d, DateTimeOffset dateTimeOffset)
        {
            if (!d.HasValue) return -1;
            return d.Value.CompareTo(dateTimeOffset);
        }

        /// <summary>
        /// Compare current <see cref="DateTime"/> with Now
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static int CompareToNow(this DateTime? d) =>
            d.CompareTo(DateTime.Now);

        /// <summary>
        /// Compare current <see cref="DateTime"/> with other
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static int CompareTo(this DateTime? d, DateTime dateTime)
        {
            if (!d.HasValue) return -1;
            return d.Value.CompareTo(dateTime);
        }

        public static string ToGoogleDate(this DateTime d)
        {
            return d.ToString("yyyyMMdd");
        }

        public static string ToGraphView(this DateTime d)
        {
            return d.ToString("dd/MM/yyyy");
        }
    }
}
