#nullable enable
using KooliProjekt.Application.DTOs;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Registrations
{
    public class GetRegistrationQuery : IRequest<OperationResult<RegistrationDto?>>
    {
        public int Id { get; set; }
    }
}