using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebPortal.ViewModels.Common
{
    public class AppIdentityResult<T>
    {
        public IdentityResult Result { get; set; }
        public T ReturnObj { get; set; }
    }
}
