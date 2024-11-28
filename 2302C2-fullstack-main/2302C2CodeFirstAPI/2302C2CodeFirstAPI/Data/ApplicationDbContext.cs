using _2302C2CodeFirstAPI.Models;

using Microsoft.EntityFrameworkCore;

namespace _2302C2CodeFirstAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<EventType> EventTypes { get; set; }


    }
}
