using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Users.Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest
    {
        public string Login { get; set; } = String.Empty;
        public bool IsFullDeleted { get; set; }
        public string RevokedBy { get; set; } = String.Empty;
    }
}
