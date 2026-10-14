using FluentValidation;
using KooliProjekt.Application.Features.Events;

namespace KooliProjekt.Application.Features.EventPrograms
{
    public class SaveEventProgramCommandValidator : AbstractValidator<SaveEvent_ProgramCommand>
    {
        public SaveEventProgramCommandValidator()
        {
            RuleFor(x => x.EventId).GreaterThan(0);
            RuleFor(x => x.Time).NotEmpty();
            RuleFor(x => x.Topic).NotEmpty().MaximumLength(255);
        }
    }
}
