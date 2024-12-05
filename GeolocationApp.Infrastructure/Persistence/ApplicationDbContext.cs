using GeolocationApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GeolocationApp.Infrastructure.Persistence
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Visit> Visits { get; set; }
    }
}