using Entity.Model;
using Entity.Paging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Contracts.Interface
{
    public interface IComponyRepository:IGenericRepository<Company>
    {
        public Company GetCompany(System.Guid COmpanyId,bool asTracking);
        public IEnumerable<Company>GetCompaniesByIds(IEnumerable< System.Guid> COmpanyId,bool asTracking);

        public void AddCompany(IEnumerable<Company> Companies);
        public Task AddCompanyAsync(Company Companies);
         public Task< Company >GetCompanyAsync(System.Guid COmpanyId,bool asTracking);
        public Task< IEnumerable<Company>>GetCompaniesByIdsasync(Expression<Func<Company,bool>>expression,bool astraking);
        public Task< IEnumerable<Company>>GetCompaniesByIdsasync(bool asTracking);
        public Task<PageList<Company>> GetCompaniesAsync(CompanyPrameter companyPrameter, bool astraking);
        
    }
}