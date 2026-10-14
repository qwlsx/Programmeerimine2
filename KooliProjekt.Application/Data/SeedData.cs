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

            GenerateEventPrograms();
            GenerateEventSummaries();
            GenerateEventFiles();
            _dbContext.SaveChanges();

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
                _dbContext.Events.Add(new Event
                {
                    Name = $"Мероприятие {i}",
                    StartDateTime = DateTime.Today.AddDays(i).AddHours(18),
                    Description = $"Описание мероприятия {i}",
                    TotalSeats = 50 + i * 10,
                    Price = 10 + i * 2,
                    CreatedAt = DateTime.UtcNow
                });
            }
        }

        private void GenerateUsers()
        {
            for (var i = 1; i <= 10; i++)
            {
                _dbContext.Users.Add(new User
                {
                    Name = $"Пользователь {i}",
                    Email = $"user{i}@test.ee",
                    Role = "User",
                    CreatedAt = DateTime.UtcNow
                });
            }
        }

        private void GenerateEventPrograms()
        {
            var events = _dbContext.Events.OrderBy(x => x.Id).Take(10).ToList();
            for (var i = 0; i < events.Count; i++)
            {
                _dbContext.Event_Programs.Add(new Event_Program
                {
                    EventId = events[i].Id,
                    Time = events[i].StartDateTime.AddMinutes(30),
                    Topic = $"Программа мероприятия {i + 1}"
                });
            }
        }

        private void GenerateEventSummaries()
        {
            var events = _dbContext.Events.OrderBy(x => x.Id).Take(10).ToList();
            for (var i = 0; i < events.Count; i++)
            {
                _dbContext.Event_Summaries.Add(new Event_Summary
                {
                    EventId = events[i].Id,
                    SummaryText = $"Итоги мероприятия {i + 1}"
                });
            }
        }

        private void GenerateEventFiles()
        {
            var events = _dbContext.Events.OrderBy(x => x.Id).Take(10).ToList();
            for (var i = 0; i < events.Count; i++)
            {
                _dbContext.Event_Files.Add(new Event_Files
                {
                    EventId = events[i].Id,
                    FileUrl = $"https://example.test/events/{events[i].Id}/info.pdf",
                    FileType = "pdf"
                });
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
                    Status = "Draft",
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
                    Status = "Draft",
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
