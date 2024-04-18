using System;
using System.Collections.Generic;
using System.Text;

namespace WebPortal.ViewModels.Common
{
    public class ApiErrorResult<T>:ApiResult<T>
    {
        public ApiErrorResult()
        {
        }

        public ApiErrorResult(string message)
        {
            Succeeded = false;
            Message = message;
        }
    }
}
