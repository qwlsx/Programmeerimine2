using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.ToDoLists
{
    public class ListToDoListsQueryHandler : IRequestHandler<ListToDoListsQuery, OperationResult<PagedResult<ToDoList>>>
    {
        private readonly ApplicationDbContext _dbContext;

        public ListToDoListsQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<PagedResult<ToDoList>>> Handle(ListToDoListsQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.Page <= 0)
            {
                throw new ArgumentException("Page must be greater than zero.", nameof(request.Page));
            }

            if (request.PageSize <= 0)
            {
                throw new ArgumentException("PageSize must be greater than zero.", nameof(request.PageSize));
            }

            if (request.PageSize > 100)
            {
                throw new ArgumentException("PageSize cannot exceed 100.", nameof(request.PageSize));
            }

            var result = new OperationResult<PagedResult<ToDoList>>();

            result.Value = await _dbContext
                .ToDoLists
                .OrderBy(list => list.Title)
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}