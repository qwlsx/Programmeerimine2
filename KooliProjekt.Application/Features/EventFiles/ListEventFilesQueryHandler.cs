using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.EventFiles
{
    public class ListEventFilesQueryHandler
        : IRequestHandler<ListEventFilesQuery, OperationResult<PagedResult<Event_Files>>>
    {
        private readonly ApplicationDbContext _context;

        public ListEventFilesQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResult<PagedResult<Event_Files>>> Handle(
            ListEventFilesQuery request,
            CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.Page <= 0)
            {
                throw new ArgumentException(
                    "Page must be greater than zero.",
                    nameof(request.Page));
            }

            if (request.PageSize <= 0)
            {
                throw new ArgumentException(
                    "PageSize must be greater than zero.",
                    nameof(request.PageSize));
            }

            if (request.PageSize > 100)
            {
                throw new ArgumentException(
                    "PageSize cannot exceed 100.",
                    nameof(request.PageSize));
            }

            var result = new OperationResult<PagedResult<Event_Files>>();

            result.Value = await _context.Event_Files
                .OrderBy(x => x.Id)
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}