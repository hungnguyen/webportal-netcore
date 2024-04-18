using System;
using System.Collections.Generic;
using System.Text;

namespace WebPortal.ViewModels.Common
{
    public class ApiResult<T>
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public T ResultObj { get; set; }
    }
}
