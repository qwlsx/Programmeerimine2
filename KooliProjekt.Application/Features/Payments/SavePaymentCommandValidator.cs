using System.Linq;
using FluentValidation;

namespace KooliProjekt.Application.Features.Payments
{
    public class SavePaymentCommandValidator : AbstractValidator<SavePaymentCommand>
    {
        public SavePaymentCommandValidator()
        {
            RuleFor(x => x.InvoiceId).GreaterThan(0);
            RuleFor(x => x.Amount).GreaterThanOrEqualTo(0);
            RuleFor(x => x.PaymentMethod).NotEmpty().MaximumLength(30);
            RuleFor(x => x.PaymentRef).MaximumLength(50);
            RuleFor(x => x.Status).Must(x => new[] { "Processing", "Completed", "Failed" }.Contains(x))
                .WithMessage("Статус платежа: Processing, Completed или Failed");
            RuleFor(x => x.PaidAt).NotNull().When(x => x.Status == "Completed")
                .WithMessage("Для завершённого платежа укажите дату оплаты");
        }
    }
}
