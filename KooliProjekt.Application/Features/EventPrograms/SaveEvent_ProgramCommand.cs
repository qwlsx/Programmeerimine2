#nullable enable
using KooliProjekt.Application.Behaviors;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using System;

namespace KooliProjekt.Application.Features.Events
{
    public class SaveEvent_ProgramCommand : IRequest<OperationResult>, ITransactional
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public DateTime Time { get; set; }
        public string Topic { get; set; } = string.Empty;
    }
}