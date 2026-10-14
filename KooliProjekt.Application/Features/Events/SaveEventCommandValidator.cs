using System;
using FluentValidation;

namespace KooliProjekt.Application.Features.Events
{
    public class SaveEventCommandValidator : AbstractValidator<SaveEventCommand>
    {
        public SaveEventCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Название мероприятия обязательно")
                .MaximumLength(150).WithMessage("Название не может быть длиннее 150 символов");
            RuleFor(x => x.StartDateTime)
                .NotEmpty().WithMessage("Дата и время начала обязательны")
                .GreaterThanOrEqualTo(_ => DateTime.Now).When(x => x.Id == 0)
                .WithMessage("Дата мероприятия не может быть в прошлом");
            RuleFor(x => x.Description).MaximumLength(1000).WithMessage("Описание не может быть длиннее 1000 символов");
            RuleFor(x => x.TotalSeats).GreaterThan(0).WithMessage("Количество мест должно быть больше нуля");
            RuleFor(x => x.Price).GreaterThanOrEqualTo(0).WithMessage("Цена не может быть отрицательной");
        }
    }
}
