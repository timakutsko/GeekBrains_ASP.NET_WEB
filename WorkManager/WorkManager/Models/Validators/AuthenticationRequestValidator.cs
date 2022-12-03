using FluentValidation;
using WorkManager.Data.Models;
using WorkManager.Requests;
using WorkManager.Responses;

namespace WorkManager.Models.Validators
{
    internal sealed class AuthenticationRequestValidator : AbstractValidator<AuthenticateRequest>
    {
        public AuthenticationRequestValidator()
        {
            RuleFor(x => x.Login)
                .NotNull()
                .Length(5, 128);

            RuleFor(x => x.Password)
                .NotNull()
                .Length(5, 15)
                .Matches(@"\@");
        }
    }
}
