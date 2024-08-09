using AutoMapper;
using IntegrationExternalParty.Data.IRepositories;
using IntegrationExternalParty.Domain.Entities;
using IntegrationExternalParty.Service.DTOs.Employees;
using IntegrationExternalParty.Service.Interfaces;

namespace IntegrationExternalParty.Service.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IRepository repository;
    private readonly IMapper mapper;

    public EmployeeService(IRepository repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async ValueTask<Employee> AddAsync(EmployeeForCreationDto model)
    {
        var employee =  this.mapper.Map<Employee>(model);
        await this.repository.InsertAsync(employee);
        return employee;
    }

    public IEnumerable<Employee> RetrieveAll()=>
         repository.SelectAllAsync();
        
    
}
