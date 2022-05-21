using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Application.Common.Validators;

namespace Users.Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommandValidator : AbstractUserValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(deleteUserCommand => deleteUserCommand.Login)
                .NotNull()
                .NotEmpty()
                .Must(IsOnlyLatinlettersAndNumbers)
                .WithMessage("{PropertyName} запрещены все символы кроме латинских букв и цифр.");

        }
    }
}
