using Microsoft.EntityFrameworkCore;
using Domain.Entities;


namespace Infrastructure.Data
{
    public class ApplicationContext : DbContext
    {
        //persistir la entidad experiencias
        public DbSet<Experiencia> Experiencias { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }
    }

}