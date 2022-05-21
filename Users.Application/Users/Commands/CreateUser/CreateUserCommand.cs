using System;
using MediatR;

namespace Users.Application.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<Guid>
    {
        public string Login { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
        public string Name { get; set; } = String.Empty;
        public int Gender { get; set; }
        public DateTime? BirthDay { get; set; }
        public bool Admin { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; } = String.Empty;

    }
}
