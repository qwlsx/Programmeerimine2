using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace KooliProjekt.Application.Features.ToDoLists
{
    // 15.11.2025
    // Kustutamise käsu händler
    public class DeleteToDoListCommandHandler : IRequestHandler<DeleteToDoListCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public DeleteToDoListCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(DeleteToDoListCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            // Kustutamine üle relatsioonide (vihje: CASCADE DELETE)
            //await _dbContext
            //    .ToDoLists
            //    .Where(t => t.Id == request.Id)
            //    .ExecuteDeleteAsync();

            // Kustutamine mitme sammuga (kahe tabeli vahel rohkem kui üks relatsioon)
            await _dbContext
                .ToDoItems
                .Where(t => t.ToDoListId == request.Id)
                .ExecuteDeleteAsync();

            await _dbContext
                .ToDoLists
                .Where(t => t.Id == request.Id)
                .ExecuteDeleteAsync();

            return result;
        }
    }
}