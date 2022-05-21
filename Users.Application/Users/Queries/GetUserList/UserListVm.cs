using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Application.Users.Queries.GetUserList
{
    public class UserListVm
    {
        public IList<UserLoockupDto> Users { get; set; }
    }
}
