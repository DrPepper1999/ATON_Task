using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Application.Common.Extensions
{
    public static class DataTimeExtension
    {
        public static bool IsUndefined(this DateTime dateTime)
        {
            return dateTime == DateTime.MinValue;
        }
    }
}
