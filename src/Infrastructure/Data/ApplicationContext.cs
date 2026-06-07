using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using ApplicationEntity = Domain.Entities.Application;

namespace Infrastructure.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Notification> Notifications { get; set; }
        public DbSet<ApplicationEntity> Applications { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

    }
}