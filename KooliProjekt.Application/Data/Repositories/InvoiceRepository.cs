namespace KooliProjekt.Application.Data.Repositories
{
    using System.Linq;
    using System.Threading.Tasks;

    public class InvoiceRepository : BaseRepository<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<string> GetNextNumberAsync()
        {
            var numbers = await Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions
                .ToListAsync(DbContext.Invoices.Select(x => x.Number));
            var lastNumber = numbers
                .Select(x => x.StartsWith("INV-") && int.TryParse(x[4..], out var number) ? number : 0)
                .DefaultIfEmpty()
                .Max();
            return $"INV-{lastNumber + 1:D5}";
        }
    }
}
