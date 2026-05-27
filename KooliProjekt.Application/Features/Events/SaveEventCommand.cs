#nullable enable
using KooliProjekt.Application.Behaviors;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using System;

namespace KooliProjekt.Application.Features.Events
{
    public class SaveEventCommand : IRequest<OperationResult>, ITransactional
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime StartDateTime { get; set; }
        public string? Description { get; set; }
        public int TotalSeats { get; set; }
        public decimal Price { get; set; }
    }
}