using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Users.Application.Users.Queries.GetUserDetails
{
    public class GetUserDetailsQuery : IRequest<UserDetailsVm>
    {
        public string Login { get; set; } = String.Empty;
    }
}
