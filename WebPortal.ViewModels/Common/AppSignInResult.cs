using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebPortal.ViewModels.Common
{
    public class AppSignInResult<T>
    {
        public SignInResult Result { get; set; }
        public T ReturnObj { get; set; }
    }
}
