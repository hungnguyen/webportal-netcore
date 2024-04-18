using System;
using System.Collections.Generic;
using System.Text;

namespace WebPortal.ViewModels.Common
{
    public class ApiSuccessResult<T> : ApiResult<T>
    {
        public ApiSuccessResult(T resultObj)
        {
            Succeeded = true;
            ResultObj = resultObj;
        }

        public ApiSuccessResult()
        {
            Succeeded = true;
        }
    }
}
