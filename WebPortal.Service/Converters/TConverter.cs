using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPortal.Services.Converters
{
    public static class TConverter
    {
        public static T ChangeType<T>(object value)
        {
            if (value == null) return default(T);

            try
            {
                var tc = TypeDescriptor.GetConverter(typeof(T));
                return (T)tc.ConvertFromInvariantString(value.ToString());
            }
            catch (NotSupportedException)
            {
                return default(T);
            }
            catch (Exception)
            {
                return default(T);
            }
        }
    }
}
