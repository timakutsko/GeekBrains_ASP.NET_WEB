using FluentValidation;
using WorkManager.Data.Models;

namespace WorkManager.Models.Validators
{
    public class ClientValidator : AbstractValidator<Client>
    {
        public ClientValidator()
        {
            RuleFor(x => x.FirstName)
                .NotNull()
                .Length(5, 128);

            RuleFor(x => x.LastName)
                .NotNull()
                .Length(5, 128);

            RuleFor(x => x.Email)
                .NotNull()
                .Length(5, 128)
                .EmailAddress();

            RuleFor(x => x.Age)
                .NotNull()
                .GreaterThan(18);
        }
    }
}
