using System;
using System.Text.RegularExpressions;
using FluentValidation;

namespace Users.Application.Common.Validators
{
    public abstract class AbstractUserValidator<T> : AbstractValidator<T>
    {
        public bool IsOnlyLatinlettersAndNumbers(string? line)
        {
            if (line is null) return true;

            return Regex.IsMatch(line, @"^[a-zA-Z0-9]+$");
        }

        public bool IsValidDate(DateTime? dateTime)
        {
            if (dateTime is not null)
            {
                return dateTime.Value <= DateTime.Now;
            }

            return true;
        }
    }
}
