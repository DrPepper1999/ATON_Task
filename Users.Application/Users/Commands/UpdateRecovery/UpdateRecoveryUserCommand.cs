using System;
using MediatR;

namespace Users.Application.Users.Commands.UpdateRecovery
{
    public class UpdateRecoveryUserCommand : IRequest
    {
        public string Login { get; set; } = String.Empty;
    }
}
