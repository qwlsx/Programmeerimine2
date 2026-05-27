using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Registrations
{
    // Мой запрос на получение списка регистраций с пагинацией
    public class ListRegistrationsQuery : IRequest<OperationResult<PagedResult<Registration>>>
    {
        // Номер текущей страницы
        public int Page { get; set; }

        // Сколько записей выводить
        public int PageSize { get; set; }
    }
}