using FluentValidation;
using WorkManager.Data.Models;

namespace WorkManager.Models.Validators
{
    public class ClientContractValidator : AbstractValidator<ClientContract>
    {
        public ClientContractValidator()
        {
            RuleFor(x => x.Title)
                .NotNull()
                .Length(5, 128);
        }
    }
}
