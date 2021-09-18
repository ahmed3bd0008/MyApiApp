using System.Collections.Generic;
namespace Entity.DataTransferObject
{
    public class CompanyDto
    {
                public System.Guid Id { get; set; }
                public string Name { get; set; }
                public string FullAddress { get; set; }
    }
     public class AddCompanyDto
    {
                public string Name { get; set; }
                public string Address { get; set; }
                public string Country { get; set; }
    }
     public class AddCompanywithEmployeesDto
    {
                public string Name { get; set; }
                public string Address { get; set; }
                public string Country { get; set; }
                public IEnumerable<AddEmployeeDto>AddEmployeeDtos{set;get;}
    }
     public class UpdateCompanyDto
    {
                public string Name { get; set; }
                public string Address { get; set; }
                public string Country { get; set; }
    }
     public class updateCompanywithEmployeesDto
    {
                public string Name { get; set; }
                public string Address { get; set; }
                public string Country { get; set; }
                public IEnumerable<UpdateEmployeeDto>updateEmployeeDtos{set;get;}
    }
}