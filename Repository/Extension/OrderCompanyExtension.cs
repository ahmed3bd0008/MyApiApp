using System.Collections.Generic;
using System.Linq;
using Entity.Model;
using Repository.Extension.Utilities;
using System.Linq.Dynamic.Core;
namespace Repository.Extension
{
    public static class OrderCompanyExtension
    {
        public static IEnumerable<Company>Sort(this IEnumerable<Company>companies,string companyOrder)
        {
            if(string.IsNullOrWhiteSpace(companyOrder))
                 return companies;
            var Orderstring= OrderStringQuery.BuiltOrderStringQuery<Company>(companyOrder);
             if(string.IsNullOrWhiteSpace(Orderstring))
                 return companies;
            return companies.AsQueryable().OrderBy(Orderstring);
        }
    }
}