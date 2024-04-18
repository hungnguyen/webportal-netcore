using System;
using System.Collections.Generic;
using System.Text;

namespace WebPortal.Utilities.Exeptions
{
    public class WPExeption : Exception
    {
        public WPExeption()
        {
        }

        public WPExeption(string message)
            : base(message)
        {
        }

        public WPExeption(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
