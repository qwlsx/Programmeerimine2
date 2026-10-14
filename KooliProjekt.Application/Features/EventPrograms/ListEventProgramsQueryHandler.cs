using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.EventPrograms
{
    public class ListEventProgramsQueryHandler
        : IRequestHandler<ListEventProgramsQuery, OperationResult<PagedResult<Event_Program>>>
    {
        private readonly ApplicationDbContext _context;

        public ListEventProgramsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResult<PagedResult<Event_Program>>> Handle(
            ListEventProgramsQuery request,
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
                    "PageSize cannot be greater than 100.",
                    nameof(request.PageSize));
            }

            var result = new OperationResult<PagedResult<Event_Program>>();

            var query = _context.Event_Programs.AsQueryable();
            if (!string.IsNullOrWhiteSpace(request.Search)) query = query.Where(x => x.Topic.Contains(request.Search));

            result.Value = await query
                .OrderBy(x => x.Id)
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}
