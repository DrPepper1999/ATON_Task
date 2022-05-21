using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Domain;

namespace Users.Application.Users.Queries.GetUserYourself
{
    public class GetUserYourselfQuery : IRequest<UserYourselfDataVm>
    {
        public string Login { get; set; }
    }
}
