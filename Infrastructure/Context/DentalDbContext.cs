using Dental.Domain.Entities.Student;
using Dental.Domain.Entities.Users;
using Dental.Infrastructure.Configurations;
using Home.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Dental.Infrastructure.Context;

public class DentalDbContext : DbContext
{
    public DentalDbContext(DbContextOptions<DentalDbContext> options):base(options)
    {
        
    }



    public DbSet<User> Users { get; set; }
    public DbSet<Student> Students { get; set; }
 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.ApplyConfiguration(new RoleConfigurations());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
    }
}
