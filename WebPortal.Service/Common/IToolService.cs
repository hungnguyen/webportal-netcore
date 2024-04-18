using System;
using System.Collections.Generic;
using System.Text;

namespace WebPortal.Services.Common
{
    public interface IToolService
    {
        string FormatPrice(object p, string currency);
        string FormatPrice(object p);
    }
}
