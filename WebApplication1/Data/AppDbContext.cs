
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Data
{
    // Контекст базы данных
    public class AppDbContext : DbContext
    {
        // Конструктор DbContext с опциями
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Таблица пользователей
        public DbSet<User> Users { get; set; } // <- здесь нужен класс User
    }

    // Минимальный класс User прямо в этом файле (если не хочешь отдельной Models)
    public class User
    {
        public int Id { get; set; }        // Primary Key
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
