using AutoMapper;
using IntegrationExternalParty.Domain.Entities;
using IntegrationExternalParty.Service.DTOs.Employees;

namespace IntegrationExternalParty.Service.Mapper;

public class MapperProfile:Profile
{
    public MapperProfile()
    {
        CreateMap<Employee, EmployeeForCreationDto>().ReverseMap();
        CreateMap<Employee,EmployeeForResultDto>().ReverseMap();
    }
}
