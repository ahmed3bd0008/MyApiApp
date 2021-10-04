using System.Collections.Generic;
using System.Dynamic;
using Contracts.Interface;
using System.Reflection;
using System.Linq;

namespace Repository.Shaping
{
            public class ShapData<T> : IShapData<T> where T : class
            {
                       private PropertyInfo[] _propertyInfos;
                        public ShapData()
                        {
                            _propertyInfos=typeof(T).GetProperties(BindingFlags.Public| BindingFlags.Instance);
                        }
                        public IEnumerable<ExpandoObject> shapData(IEnumerable<T> Entities, string Fields)
                        {
                                   IEnumerable<PropertyInfo>propertyInfos=getRequiredProperties(Fields);
                                   IEnumerable<ExpandoObject> shaps=FetchData(propertyInfos,Entities);
                                   return shaps;
                        }

                        public ExpandoObject shapData(T entity, string Field)
                        {
                                   IEnumerable<PropertyInfo>propertyInfos=getRequiredProperties(Field);
                                   ExpandoObject shaps=FetchDataForEntity(propertyInfos,entity);
                                   return shaps;
                        }
                        private IEnumerable<PropertyInfo> getRequiredProperties(string Fields)
                        {
                             List<PropertyInfo>propertyInfos=new List<PropertyInfo>();
                             if (!string.IsNullOrWhiteSpace(Fields))
                             {
                                   string [] fieldsRequired=Fields.Split(',',System.StringSplitOptions.RemoveEmptyEntries) ; 
                                   foreach (var field in fieldsRequired)
                                   {
                                        var Property=_propertyInfos.FirstOrDefault(pro=>pro.Name.Equals(field.Trim(),
                                             System.StringComparison.InvariantCultureIgnoreCase));
                                             if(Property==null)
                                                  continue;
                                        propertyInfos.Add(Property);
                                   }  
                             }
                             else{
                                        propertyInfos=_propertyInfos.ToList();
                             }
                             return propertyInfos;
                        }
                       private ExpandoObject FetchDataForEntity(IEnumerable<PropertyInfo> propertyInfos,T Entity)
                       {
                            ExpandoObject shapObject=new();
                            foreach (var item in propertyInfos)
                            {
                                var objectPropertiesValue=item.GetValue(Entity);
                                shapObject.TryAdd(item.Name,objectPropertiesValue);
                            }
                            return shapObject;
                       }
                       private IEnumerable<ExpandoObject> FetchData(IEnumerable<PropertyInfo>propertyInfos,IEnumerable<T> Entities)
                       {
                            List<ExpandoObject> ShapsData=new();
                            foreach (var Entity in Entities)
                            {
                                var shap=FetchDataForEntity(propertyInfos,Entity);
                                ShapsData.Add(shap);
                           
                            }
                        return ShapsData;
                       }
            }
}