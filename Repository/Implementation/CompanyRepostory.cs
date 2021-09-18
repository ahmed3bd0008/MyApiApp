using System;
using System.Collections.Generic;
using System.Linq;
using Contracts.Interface;
using Entity.Context;
using Entity.Model;

namespace Repository.Implementation
{
    public class CompanyRepository:GenericRepository<Company>,IComponyRepository
    {
        public CompanyRepository(RepoDbContext Context):base(Context)
        {
                            
        }

        public IEnumerable<Company> GetCompaniesByIds(IEnumerable<Guid> COmpanyId, bool asTracking)
        {
          return  FindByCondation(opt => COmpanyId.Contains(opt.Id),asTracking);
        }

        public Company GetCompany(Guid COmpanyId, bool asTracking)
        {
          return   FindByCondation(d=>d.Id==COmpanyId,asTracking).SingleOrDefault();
        }
        public void AddCompany(IEnumerable<Company>Companies)
        {
            foreach (var company in Companies)
            {
                Create(company);
            }
        }

      
    }
}