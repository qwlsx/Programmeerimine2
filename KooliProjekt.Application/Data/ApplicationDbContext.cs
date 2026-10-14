#nullable enable
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Event> Events { get; set; } = null!;
        public DbSet<Registration> Registrations { get; set; } = null!;
        public DbSet<Event_Program> Event_Programs { get; set; } = null!;
        public DbSet<Event_Summary> Event_Summaries { get; set; } = null!;
        public DbSet<Event_Files> Event_Files { get; set; } = null!;
        public DbSet<Invoice> Invoices { get; set; } = null!;
        public DbSet<Payment> Payments { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasIndex(x => x.Email)
                .IsUnique();

            modelBuilder.Entity<Event_Summary>()
                .HasIndex(x => x.EventId)
                .IsUnique();

            modelBuilder.Entity<Invoice>()
                .HasIndex(x => x.RegistrationId)
                .IsUnique();

            modelBuilder.Entity<Invoice>()
                .HasIndex(x => x.Number)
                .IsUnique();

            modelBuilder.Entity<Payment>()
                .HasIndex(x => x.PaymentRef)
                .IsUnique()
                .HasFilter("[PaymentRef] IS NOT NULL");
        }
    }
}
