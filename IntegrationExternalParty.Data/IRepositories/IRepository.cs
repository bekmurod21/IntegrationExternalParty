using IntegrationExternalParty.Domain.Entities;
using System.Linq.Expressions;

namespace IntegrationExternalParty.Data.IRepositories
{
    public interface IRepository
    {
        ValueTask<Employee> InsertAsync(Employee employee);
        IEnumerable<Employee> SelectAllAsync();
    }
}
