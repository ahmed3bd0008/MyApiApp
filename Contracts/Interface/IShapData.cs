using System.Collections.Generic;
using System.Dynamic;

namespace Contracts.Interface
{
    public interface IShapData <T>
    {
             public IEnumerable<ExpandoObject>shapData(IEnumerable<T>Entities,string Fields)  ;
             public ExpandoObject shapData(T entity,string Field);
    }
}