namespace KooliProjekt.Application.Data.Repositories
{
    public class RegistrationRepository : BaseRepository<Registration>, IRegistrationRepository
    {
        public RegistrationRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}