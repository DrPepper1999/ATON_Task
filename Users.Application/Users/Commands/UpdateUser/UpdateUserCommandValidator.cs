using System;
using FluentValidation;
using Users.Application.Common.Validators;

namespace Users.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractUserValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(updateUserCommand => updateUserCommand)
                .NotNull();
            RuleFor(updateUserCommand => updateUserCommand.Login)
                .Must(IsOnlyLatinlettersAndNumbers)
                .WithMessage("{PropertyName} запрещены все символы кроме латинских букв и цифр.");
            RuleFor(updateUserCommand => updateUserCommand.Password)
                .Must(IsOnlyLatinlettersAndNumbers)
                .WithMessage("{PropertyName} запрещены все символы кроме латинских букв и цифр.");
            RuleFor(updateUserCommand => updateUserCommand.Name)
                .Must(IsOnlyLatinlettersAndNumbers)
                .WithMessage("{PropertyName} запрещены все символы кроме латинских букв и цифр.");
            RuleFor(updateUserCommand => updateUserCommand.Gender)
                .InclusiveBetween(0, 3);
            RuleFor(updateUserCommand => updateUserCommand.BirthDay)
                .Must(birthDay => IsValidDate(birthDay));
            RuleFor(updateUserCommand => updateUserCommand.ModifiedBy)
                .NotNull()
                .NotEmpty();
        }

    }
}
