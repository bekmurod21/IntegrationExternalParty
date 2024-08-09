using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using IntegrationExternalParty.Data.Contexts;
using IntegrationExternalParty.Data.IRepositories;
using IntegrationExternalParty.Domain.Entities;

namespace IntegrationExternalParty.Data.Repositories
{
    public class Repository : IRepository
    {
        private readonly AppDbContext dbContext;
        private readonly DbSet<Employee> dbSet;
        public Repository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = dbContext.Set<Employee>();
        }

        public async ValueTask<Employee> InsertAsync(Employee value)
        {
            var entity = (await dbSet.AddAsync(value)).Entity;
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public IEnumerable<Employee> SelectAllAsync()
        {
            return dbSet.AsNoTracking().AsEnumerable();           
        }
    }
}
