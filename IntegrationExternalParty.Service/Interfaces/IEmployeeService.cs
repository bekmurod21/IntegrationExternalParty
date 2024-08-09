
using IntegrationExternalParty.Domain.Entities;
using IntegrationExternalParty.Service.DTOs.Employees;

namespace IntegrationExternalParty.Service.Interfaces
{
    public interface IEmployeeService
    {
        ValueTask<Employee> AddAsync(EmployeeForCreationDto model);
        IEnumerable<Employee> RetrieveAll();
    }
}
