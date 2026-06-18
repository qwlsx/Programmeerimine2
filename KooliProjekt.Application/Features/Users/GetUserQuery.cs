#nullable enable
using KooliProjekt.Application.DTOs;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Users
{
    public class GetUserQuery : IRequest<OperationResult<UserDto?>>
    {
        public int Id { get; set; }
    }
}