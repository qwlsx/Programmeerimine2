using System.Linq;
using FluentValidation;

namespace KooliProjekt.Application.Features.Registrations
{
    public class SaveRegistrationCommandValidator : AbstractValidator<SaveRegistrationCommand>
    {
        public SaveRegistrationCommandValidator()
        {
            RuleFor(x => x.UserId).GreaterThan(0);
            RuleFor(x => x.EventId).GreaterThan(0);
            RuleFor(x => x.Status).Must(x => new[] { "Draft", "Paid", "Cancelled" }.Contains(x))
                .WithMessage("Статус регистрации: Draft, Paid или Cancelled");
            RuleFor(x => x.RegisteredAt).NotEmpty();
        }
    }
}
