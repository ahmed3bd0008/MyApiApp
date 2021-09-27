using Entity.Model;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Linq.Dynamic.Core;

namespace Repository.Extension
{
  public static class OrderEmployeExtension
    {
        public static IQueryable<Employee> Sort(this IQueryable<Employee> employees,string OrderString)
        {
            if (string.IsNullOrWhiteSpace( OrderString))
                return employees.OrderBy(d => d.Name);
            string[] OrderPrams = OrderString.Trim().Split(',');
            PropertyInfo[] EmployeeProrties = typeof(Employee).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            StringBuilder orderQuery = new();
            foreach (var Parsm in OrderPrams)
            {
                string OrderField = Parsm.Trim().Split(' ')[0].Trim();
                if (string.IsNullOrWhiteSpace(OrderField))
                    continue;
                PropertyInfo EmployeeColumn=EmployeeProrties.FirstOrDefault(Pr=>Pr.Name.Trim().Equals(OrderField, System.StringComparison.InvariantCultureIgnoreCase));
                if (EmployeeColumn==null)
                    continue;
                var direction= Parsm.EndsWith("desc")? "descending" : "ascending";
                orderQuery.Append($"{EmployeeColumn.Name} {direction},");
            }
            var OrderByQuery = orderQuery.ToString().TrimEnd(',', ' ');
            if (string.IsNullOrWhiteSpace(OrderByQuery))
               return employees.OrderBy(d => d.Name);
            return employees.AsQueryable().OrderBy(OrderByQuery); 

        }
    }
}
