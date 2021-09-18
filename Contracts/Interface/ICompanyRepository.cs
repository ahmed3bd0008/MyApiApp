using Entity.Model;
using System.Collections.Generic;
namespace Contracts.Interface
{
    public interface IComponyRepository:IGenericRepository<Company>
    {
        public Company GetCompany(System.Guid COmpanyId,bool asTracking);
        public IEnumerable<Company>GetCompaniesByIds(IEnumerable< System.Guid> COmpanyId,bool asTracking);

        public void AddCompany(IEnumerable<Company> Companies);
    }
}