using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using WebPortal.Services.Converters;

namespace WebPortal.Services.Common
{
    public class ToolService : IToolService
    {
        public string FormatPrice(object p, string currency)
        {
			if (p != null && !string.IsNullOrEmpty(p.ToString()))
				return $"{currency} {TConverter.ChangeType<decimal>(p).ToString("#,###").Replace(",", ".")}";
			else
				return string.Empty;
		}

        public string FormatPrice(object p)
        {
			return FormatPrice(p, "₫");
        }
    }
}
