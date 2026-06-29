using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using ApplicationEntity = Domain.Entities.Application;

namespace Infrastructure.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Cv> Cvs { get; set; }

        public DbSet<Notification> Notifications { get; set; }
        public DbSet<ApplicationEntity> Applications { get; set; }

        public DbSet<ApplicationHistory> ApplicationHistories { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Education> Educations { get; set; }

        public DbSet<JobOffer> JobOffers { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

    }
}