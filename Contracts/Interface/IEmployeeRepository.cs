using System;
using Entity.Model;
using System.Collections.Generic;
using Entity.Paging;
using System.Threading.Tasks;

namespace Contracts.Interface
{
    public interface IEmployeeRepository:IGenericRepository<Employee>
    {
           public Employee GetEmployee(System.Guid EmployeeId, bool asTracking);
            public IEnumerable<Employee> GetEmployeesByCompany(Guid CompanyeId, bool asTracking);
            public Task <IEnumerable<Employee>> GetEmployeesByCompany(Guid CompanyeId,EmployeePrameter employeePrameter , bool asTracking);
            public Task <PageList<Employee>> GetEmployeesByCompanyPaging(Guid CompanyeId,EmployeePrameter employeePrameter , bool asTracking);
            public Task <PageList<Employee>> GetEmployeesByCompanyFilter(Guid CompanyeId,EmployeePrameter employeePrameter , bool asTracking);
            public Employee GetEmployeeByCompany(Guid CompanyId,Guid EmployeeId, bool asTracking);
            public void CreateEmployee(Guid CompanyId,Employee Employee);
            public void CreateEmployeeAsync(Guid CompanyId,Employee Employee);
    }
}