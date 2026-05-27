#nullable enable
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Users
{
    public class GetUserQuery : IRequest<OperationResult<User?>>
    {
        public int Id { get; set; }
    }
}