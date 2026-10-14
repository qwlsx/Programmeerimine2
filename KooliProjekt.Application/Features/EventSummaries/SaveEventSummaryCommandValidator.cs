using FluentValidation;

namespace KooliProjekt.Application.Features.EventSummaries
{
    public class SaveEventSummaryCommandValidator : AbstractValidator<SaveEvent_SummaryCommand>
    {
        public SaveEventSummaryCommandValidator()
        {
            RuleFor(x => x.EventId).GreaterThan(0);
            RuleFor(x => x.SummaryText).NotEmpty();
        }
    }
}
