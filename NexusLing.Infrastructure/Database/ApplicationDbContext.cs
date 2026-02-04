using Microsoft.EntityFrameworkCore;
using NexusLing.Application.Common.Interfaces.Database;
using NexusLing.Domain.Common;
using NexusLing.Domain.Entities;

namespace NexusLing.Infrastructure.Database
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<User> Users { get; set; }

        //public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        //    : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=NexusLing;Username=postgres;Password=123456");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType)
                        .Property("Id")
                        .ValueGeneratedOnAdd()
                        .IsRequired();
                }
            }
        }
    }
}
