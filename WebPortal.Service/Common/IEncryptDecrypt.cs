using System;
using System.Collections.Generic;
using System.Text;

namespace WebPortal.Services.Common
{
    public interface IEncryptDecrypt
    {
        string Encrypt(string text, string keyString);
        string Decrypt(string text, string keyString);
    }
}
