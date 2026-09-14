using Microsoft.EntityFrameworkCore;
using KooliProjekt.Application.Data;

namespace KooliProjekt.Application.UnitTests
{
    public class TestBase
    {
        protected ApplicationDbContext GetFaultyDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>();

            var dbContext = new ApplicationDbContext(options.Options);

            return dbContext;
        }
    }
}