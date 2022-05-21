using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Users.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest
    {
        public string UserLogin { get; set; } = String.Empty;
        public string? Login { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }
        public int? Gender { get; set; }
        public DateTime? BirthDay { get; set; }
        public string ModifiedBy { get; set; } = String.Empty;
    }
}
