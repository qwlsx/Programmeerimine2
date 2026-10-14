using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Registrations
{
    public class ListRegistrationsQueryHandler : IRequestHandler<ListRegistrationsQuery, OperationResult<PagedResult<Registration>>>
    {
        private readonly ApplicationDbContext _context;

        public ListRegistrationsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResult<PagedResult<Registration>>> Handle(ListRegistrationsQuery request, CancellationToken cancellationToken)
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

            var result = new OperationResult<PagedResult<Registration>>();

            var query = _context.Registrations.AsQueryable();
            if (!string.IsNullOrWhiteSpace(request.Search)) query = query.Where(x => x.Status.Contains(request.Search));

            result.Value = await query
                .OrderBy(x => x.Id)
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}
