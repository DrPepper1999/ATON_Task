using System;
using FluentValidation;
using Users.Application.Common.Validators;

namespace Users.Application.Users.Commands.UpdateRecovery
{
    public class UpdateRecoveryUserCommandValidator : AbstractUserValidator<UpdateRecoveryUserCommand>
    {
        public UpdateRecoveryUserCommandValidator()
        {
            RuleFor(deleteUserCommand => deleteUserCommand.Login)
                .NotNull()
                .NotEmpty()
                .Must(IsOnlyLatinlettersAndNumbers)
                .WithMessage("{PropertyName} запрещены все символы кроме латинских букв и цифр.");

        }
    }
}
