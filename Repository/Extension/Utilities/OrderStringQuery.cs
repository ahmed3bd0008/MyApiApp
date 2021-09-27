using System.Linq;
using System.Reflection;
using System.Text;
namespace Repository.Extension.Utilities
{
    public static class OrderStringQuery
    {
            public static string BuiltOrderStringQuery<T>(string OrderString)
            {
                        string[]OrderPrams=OrderString.Trim().Split(',');
                        PropertyInfo []Properties=typeof(T).GetProperties(BindingFlags.Public|BindingFlags.Instance);
                        StringBuilder BuildStringQuery=new ();
                        foreach (var parmas in OrderPrams)
                        {
                            var OrderField=parmas.Trim().Split(' ')[0].Trim();
                            if(string.IsNullOrWhiteSpace(OrderField))
                                    continue;
                            var ObjectColumn=Properties.FirstOrDefault(obj=>obj.Name.Trim().Equals(OrderField,System.StringComparison.CurrentCultureIgnoreCase));
                            if(ObjectColumn==null)
                                    continue;
                            var direct=parmas.EndsWith(" desc")?"descending" : "ascending";
                            BuildStringQuery.Append($"{ObjectColumn.Name} {direct},");
                        }
                        return  BuildStringQuery.ToString().TrimEnd(',',' ');
            }
    }
}