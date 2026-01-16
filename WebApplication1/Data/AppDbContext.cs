
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Data
{
  
    public class AppDbContext : DbContext
    {
   
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
    }
    public class ListToDo
    {
        public int Id { get; set; }        
        public string? Text { get; set; }

    }
}
