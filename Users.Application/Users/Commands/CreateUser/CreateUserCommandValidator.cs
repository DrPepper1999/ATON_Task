using System;
using System.Text.RegularExpressions;
using Users.Application.Common.Validators;
using FluentValidation;

namespace Users.Application.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractUserValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(createUserCommand => createUserCommand.Login)
                .NotEmpty()
                .Must(IsOnlyLatinlettersAndNumbers)
                .NotNull()
                .WithMessage("{PropertyName} запрещены все символы кроме латинских букв и цифр.");
            RuleFor(createUserCommand => createUserCommand.Password)
                .NotEmpty()
                .NotNull()
                .Must(IsOnlyLatinlettersAndNumbers)
                .WithMessage("{PropertyName} запрещены все символы кроме латинских букв и цифр.");
            RuleFor(createUserCommand => createUserCommand.Name)
               .NotEmpty()
               .NotNull()
               .Must(IsOnlyLatinlettersAndNumbers)
               .WithMessage("{PropertyName} запрещены все символы кроме латинских букв и цифр.");
            RuleFor(createUserCommand => createUserCommand.Gender)
               .InclusiveBetween(0, 3);
            RuleFor(updateUserCommand => updateUserCommand.BirthDay)
                .Must(birthDay => IsValidDate(birthDay));
            RuleFor(createUserCommand => createUserCommand.BirthDay)
               .NotEmpty();
        }
    }
}
