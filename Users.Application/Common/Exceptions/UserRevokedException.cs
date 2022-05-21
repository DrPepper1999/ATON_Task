using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Application.Common.Exceptions
{
    public class UserRevokedException : Exception
    {
        public UserRevokedException(string name, object key)
    : base($"Entity \"{name}\" ({key}) Revoked.") { }
    }
}
