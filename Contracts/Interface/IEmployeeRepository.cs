using System;
using Entity.Model;
using System.Collections.Generic;
namespace Contracts.Interface
{
    public interface IEmployeeRepository:IGenericRepository<Employee>
    {
           public Employee GetEmployee(System.Guid EmployeeId, bool asTracking);
            public IEnumerable<Employee> GetEmployeesByCompany(Guid CompanyeId, bool asTracking);
            public Employee GetEmployeeByCompany(Guid CompanyId,Guid EmployeeId, bool asTracking);
            public void CreateEmployee(Guid CompanyId,Employee Employee);
    }
}