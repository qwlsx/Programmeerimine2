using FluentValidation;

namespace KooliProjekt.Application.Features.EventFiles
{
    public class SaveEventFileCommandValidator : AbstractValidator<SaveEvent_FileCommand>
    {
        public SaveEventFileCommandValidator()
        {
            RuleFor(x => x.EventId).GreaterThan(0);
            RuleFor(x => x.FileUrl).NotEmpty().MaximumLength(500);
            RuleFor(x => x.FileType).NotEmpty().MaximumLength(10);
        }
    }
}
