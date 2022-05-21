using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Application.Common.Exceptions
{
    public class NonUniqueFieldException : Exception
    {
        public NonUniqueFieldException(string name, object key)
            : base($"Entity \"{name}\" (not-unique field {key}).") { }
    }
}
