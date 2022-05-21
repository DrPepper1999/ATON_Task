using System;
using FluentValidation;
using Users.Application.Common.Validators;
using Users.Application.Users.Queries.GetUserGreaterThen;

namespace Users.Application.Users.Queries.GetUserListGreaterThen
{
    public class GetUserListGreaterThenQueryValidator : AbstractUserValidator<GetUserListGreaterThenQuery>
    {
        public GetUserListGreaterThenQueryValidator()
        {
            RuleFor(user => user.Age)
              .NotNull()
              .GreaterThanOrEqualTo(0);
        }
    }
}
