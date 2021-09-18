using System;
using System.Linq;
using Contracts.Interface;
using Entity.Context;
using Entity.Model;
using System.Collections.Generic;

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
            }
}