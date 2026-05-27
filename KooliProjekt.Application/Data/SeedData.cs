using System;
using System.Linq;
using KooliProjekt.Application.Data;

namespace KooliProjekt.Application.Data
{
    public class SeedData
    {
        private readonly ApplicationDbContext _dbContext;

        public SeedData(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Generate()
        {
            if (_dbContext.Events.Any()) return;

            // 1. Создаем базовые сущности (они ни от кого не зависят)
            GenerateEvents();
            GenerateUsers();
            _dbContext.SaveChanges(); // Сохраняем, чтобы получить ID

            // 2. Теперь создаем зависимые
            GenerateRegistrations();
            _dbContext.SaveChanges();

            GenerateInvoices();
            _dbContext.SaveChanges();

            GeneratePayments();
            _dbContext.SaveChanges();
        }

        private void GenerateEvents()
        {
            for (var i = 1; i <= 10; i++)
            {
                _dbContext.Events.Add(new Event { Name = $"Событие {i}", TotalSeats = 100 });
            }
        }

        private void GenerateUsers()
        {
            for (var i = 1; i <= 10; i++)
            {
                _dbContext.Users.Add(new User { Name = $"Пользователь {i}", Email = $"user{i}@test.ee", Role = "User" });
            }
        }

        private void GenerateRegistrations()
        {
            var users = _dbContext.Users.ToList();
            var events = _dbContext.Events.ToList();

            for (var i = 0; i < 10; i++)
            {
                _dbContext.Registrations.Add(new Registration
                {
                    UserId = users[i].Id,
                    EventId = events[i].Id,
                    Status = "Active",
                    RegisteredAt = DateTime.Now
                });
            }
        }

        private void GenerateInvoices()
        {
            var regs = _dbContext.Registrations.ToList();
            for (var i = 0; i < 10; i++)
            {
                _dbContext.Invoices.Add(new Invoice
                {
                    RegistrationId = regs[i].Id,
                    Number = $"INV-{100 + i}",
                    TotalAmount = 50.0m + i,
                    Status = "Unpaid",
                    CreatedAt = DateTime.Now
                });
            }
        }

        private void GeneratePayments()
        {
            var invs = _dbContext.Invoices.ToList();
            for (var i = 0; i < 10; i++)
            {
                _dbContext.Payments.Add(new Payment
                {
                    InvoiceId = invs[i].Id,
                    Amount = invs[i].TotalAmount,
                    PaymentMethod = "Bank",
                    Status = "Completed",
                    PaidAt = DateTime.Now
                });
            }
        }
    }
}