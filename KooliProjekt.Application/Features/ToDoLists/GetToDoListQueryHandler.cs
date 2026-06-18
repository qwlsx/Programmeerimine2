using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.ToDoLists
{
    public class GetToDoListQueryHandler : IRequestHandler<GetToDoListQuery, OperationResult<object>>
    {
        private readonly IToDoListRepository _toDoListRepository;

        public GetToDoListQueryHandler(IToDoListRepository toDoListRepository)
        {
            _toDoListRepository = toDoListRepository;
        }

        public async Task<OperationResult<object>> Handle(GetToDoListQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<object>();

            if (request == null)
            {
                return result;
            }

            var list = await _toDoListRepository.GetByIdAsync(request.Id);

            if (list == null)
            {
                return result;
            }

            result.Value = new
            {
                Id = list.Id,
                Title = list.Title,
                Items = list.Items?.Select(item => new
                {
                    Id = item.Id,
                    Title = item.Title,
                    IsDone = item.IsDone
                })
            };

            return result;
        }
    }
}