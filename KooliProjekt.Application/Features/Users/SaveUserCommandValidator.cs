using FluentValidation;

namespace KooliProjekt.Application.Features.Users
{
    public class SaveUserCommandValidator : AbstractValidator<SaveUserCommand>
    {
        public SaveUserCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(100);
            RuleFor(x => x.Role).NotEmpty().MaximumLength(20);
        }
    }
}
