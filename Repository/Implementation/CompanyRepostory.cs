using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Contracts.Interface;
using Entity.Context;
using Entity.Model;
using Microsoft.EntityFrameworkCore;

namespace Repository.Implementation
{
    public class CompanyRepository:GenericRepository<Company>,IComponyRepository
    {
      private readonly RepoDbContext _Context;

        public CompanyRepository(RepoDbContext Context):base(Context)
        {
                  _Context=Context;          
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

        public async Task<Company> GetCompanyAsync(Guid COmpanyId, bool asTracking)
        {
                  return await FindByCondation(Opt=>Opt.Id.Equals(COmpanyId),false).SingleOrDefaultAsync();
        }

      
        public async Task<IEnumerable<Company>> GetCompaniesByIdsasync(bool asTracking)
        {
                    return  await FindAll(false).ToListAsync();
        }

        public async Task<IEnumerable<Company>> GetCompaniesByIdsasync(Expression<Func<Company,bool>> expression,bool astraking)
        {
                    return await FindByCondation(expression,astraking).ToListAsync();
        }

       public async Task AddCompanyAsync(Company Companies)
       {
                 await  _Context.Companies.AddAsync(Companies);
       }
            }
}