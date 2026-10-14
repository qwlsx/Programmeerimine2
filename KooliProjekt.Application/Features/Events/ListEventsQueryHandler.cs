using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.DTOs;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Events
{
    public class ListEventsQueryHandler : IRequestHandler<ListEventsQuery, OperationResult<PagedResult<EventDto>>>
    {
        private readonly ApplicationDbContext _context;

        public ListEventsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResult<PagedResult<EventDto>>> Handle(ListEventsQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (request.Page <= 0) throw new ArgumentException("Page must be greater than zero.", nameof(request.Page));
            if (request.PageSize <= 0) throw new ArgumentException("PageSize must be greater than zero.", nameof(request.PageSize));
            if (request.PageSize > 100) throw new ArgumentException("PageSize cannot exceed 100.", nameof(request.PageSize));

            var query = _context.Events.AsQueryable();
            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                query = query.Where(x => x.Name.Contains(request.Search) || (x.Description != null && x.Description.Contains(request.Search)));
            }

            var page = await query
                .OrderBy(x => x.Name)
                .Select(x => new EventDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    StartDateTime = x.StartDateTime,
                    Description = x.Description,
                    TotalSeats = x.TotalSeats,
                    Price = x.Price,
                    CreatedAt = x.CreatedAt
                })
                .GetPagedAsync(request.Page, request.PageSize);

            return new OperationResult<PagedResult<EventDto>>(page);
        }
    }
}
