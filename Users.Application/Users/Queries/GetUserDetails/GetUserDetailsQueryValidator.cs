using System;
using FluentValidation;
using Users.Application.Common.Validators;

namespace Users.Application.Users.Queries.GetUserDetails
{
    public class GetUserDetailsQueryValidator : AbstractUserValidator<GetUserDetailsQuery>
    {
        public GetUserDetailsQueryValidator()
        {
            RuleFor(user => user.Login)
               .NotEmpty()
               .NotNull()
               .Must(IsOnlyLatinlettersAndNumbers)
               .WithMessage("{PropertyName} запрещены все символы кроме латинских букв и цифр.");
        }
    }
}
