using TinyCsvParser.Mapping;
using IntegrationExternalParty.Domain.Entities;
using IntegrationExternalParty.Service.DTOs.Employees;

namespace IntegrationExternalParty.Service.Mapper
{
    public class CsvUserDetailsMapping : CsvMapping<EmployeeForCreationDto>
    {
        public CsvUserDetailsMapping()
            : base()
        {
            MapProperty(0, x => x.PayrollNumber);
            MapProperty(1, x => x.FirstName);
            MapProperty(2, x => x.LastName);
            MapProperty(3, x => x.BirthOfDate);
            MapProperty(4, x => x.Telephone);
            MapProperty(5, x => x.Mobile);
            MapProperty(6, x => x.Address);
            MapProperty(7, x => x.Address2);
            MapProperty(8, x => x.PostCode);
            MapProperty(9, x => x.Email);
            MapProperty(10, x => x.StartDate);
        }
    }
}
