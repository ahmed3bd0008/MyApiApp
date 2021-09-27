using System;
using System.Linq;
using Contracts.Interface;
using Entity.Context;
using Entity.Model;
using System.Collections.Generic;
using Entity.Paging;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Repository.Extension;
namespace Repository.Implementation
{
    public class EmployeeRepository:GenericRepository<Employee>,IEmployeeRepository
    {
                public EmployeeRepository(RepoDbContext Context):base(Context)
                {
                    
                }

                        public void CreateEmployee(Guid CompanyId, Employee Employee)
                        {
                                Employee.CompanyId=CompanyId;
                                Create(Employee);
                        }

                        public void CreateEmployeeAsync(Guid CompanyId, Employee Employee)
                        {
                                    throw new NotImplementedException();
                        }

                        public Employee GetEmployee(Guid EmployeeId, bool asTracking)
                        {
                                 return FindByCondation(d=>d.Id.Equals(EmployeeId),asTracking).SingleOrDefault();
                        }

                        public Employee GetEmployeeByCompany(Guid CompanyId, Guid EmployeeId, bool asTracking)
                        {
                                    return FindByCondation(d=>d.CompanyId.Equals(CompanyId)&& d.Id.Equals(EmployeeId),asTracking).SingleOrDefault();
                        }

                        public IEnumerable<Employee> GetEmployeesByCompany(Guid CompanyId, bool asTracking)
                        {
                                 return FindByCondation(d=>d.CompanyId.Equals(CompanyId),asTracking);
                        }

                        public async Task <IEnumerable<Employee>> GetEmployeesByCompany(Guid CompanyeId, EmployeePrameter employeePrameter, bool asTracking)
                        {
                               return await    FindByCondation(d=>d.CompanyId.Equals(CompanyeId),asTracking).
                                    OrderBy(e=>e.Name).
                                    Skip((employeePrameter.PageNumber-1)*10).
                                    Take(employeePrameter.PageSize).
                                    ToListAsync();
                        }

                        public async Task<PageList<Employee>> GetEmployeesByCompanyFilter(Guid CompanyeId, EmployeePrameter employeePrameter, bool asTracking)
                        {
                                   var Employee=await FindByCondation(d=>d.CompanyId.Equals(CompanyeId),asTracking).
                                   Where(d=>d.Age>=employeePrameter.MinAge &&d.Age<employeePrameter.MaxAge &&
                                                     (employeePrameter.Name==null||d.Name.ToLower().Contains(employeePrameter.Name.ToLower()))).
                                                     Sort(employeePrameter.OrderString).
                                                     ToListAsync();
                                return PageList<Employee>.
                                ToPageList(Employee,employeePrameter.PageSize,employeePrameter.PageNumber);
                        }
                        public async Task<PageList<Employee>> GetEmployeesByCompanyPaging(Guid CompanyeId, EmployeePrameter employeePrameter, bool asTracking)
                        { 
                                var Employee= await FindByCondation(d=>d.CompanyId.Equals(CompanyeId),asTracking).
                                OrderBy(d=>d.Name).
                                ToListAsync();
                                return PageList<Employee>.
                                ToPageList(Employee,employeePrameter.PageSize,employeePrameter.PageNumber);
                                
                        }
    } 
}