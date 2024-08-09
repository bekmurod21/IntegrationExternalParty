using IntegrationExternalParty.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace IntegrationExternalParty.Data.Contexts;

public class AppDbContext:DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
    public virtual DbSet<Employee> Employees { get; set; }
    
}
