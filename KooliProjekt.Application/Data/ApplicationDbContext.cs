#nullable enable
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Старые таблицы-примеры (чтобы ничего не сломать)
        public DbSet<ToDoList> ToDoLists { get; set; } = null!;
        public DbSet<ToDoItem> ToDoItems { get; set; } = null!;

        // новые 8 таблиц для мероприятий
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Event> Events { get; set; } = null!;
        public DbSet<Registration> Registrations { get; set; } = null!;
        public DbSet<Event_Program> Event_Programs { get; set; } = null!;
        public DbSet<Event_Summary> Event_Summaries { get; set; } = null!;
        public DbSet<Event_Files> Event_Files { get; set; } = null!;
        public DbSet<Invoice> Invoices { get; set; } = null!;
        public DbSet<Payment> Payments { get; set; } = null!;
    }
}