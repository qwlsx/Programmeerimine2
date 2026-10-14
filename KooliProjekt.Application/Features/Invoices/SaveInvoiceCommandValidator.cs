using System.Linq;
using FluentValidation;

namespace KooliProjekt.Application.Features.Invoices
{
    public class SaveInvoiceCommandValidator : AbstractValidator<SaveInvoiceCommand>
    {
        public SaveInvoiceCommandValidator()
        {
            RuleFor(x => x.RegistrationId).GreaterThan(0);
            RuleFor(x => x.Number).NotEmpty().When(x => x.Id > 0);
            RuleFor(x => x.Number).MaximumLength(20);
            RuleFor(x => x.TotalAmount).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Status).Must(x => new[] { "Draft", "Final", "Cancelled" }.Contains(x))
                .WithMessage("Статус счёта: Draft, Final или Cancelled");
        }
    }
}
