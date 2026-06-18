#nullable enable
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.DTOs;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.ToDoLists
{
    public class GetToDoListQueryHandler : IRequestHandler<GetToDoListQuery, OperationResult<ToDoListDto?>>
    {
        private readonly IToDoListRepository _repository;
        public GetToDoListQueryHandler(IToDoListRepository repository) => _repository = repository;

        public async Task<OperationResult<ToDoListDto?>> Handle(GetToDoListQuery request, CancellationToken ct)
        {
            var result = new OperationResult<ToDoListDto?>();
            if (request == null) return result;

            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity != null)
            {
                result.Value = new ToDoListDto
                {
                    Id = entity.Id,
                    Title = entity.Title,
                    Items = entity.Items?.Select(i => new ToDoItemDto { Id = i.Id, Title = i.Title, IsDone = i.IsDone }).ToList() ?? new()
                };
            }
            return result;
        }
    }
}