using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Users.Application.Users.Queries.GetUserList
{
    public class GetUserListQuery : IRequest<UserListVm>
    {

    }
}
