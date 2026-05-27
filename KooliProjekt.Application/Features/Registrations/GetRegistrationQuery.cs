#nullable enable
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Registrations
{
    public class GetRegistrationQuery : IRequest<OperationResult<Registration?>>
    {
        public int Id { get; set; }
    }
}