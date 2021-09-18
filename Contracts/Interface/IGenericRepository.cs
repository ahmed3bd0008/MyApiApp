
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Contracts.Interface
{
    public interface IGenericRepository<T>
    {
        public void Create(T Model);
        public void Delete(T Model);
        public void Update(T Model);
        public IQueryable<T> FindAll(bool AsTracking);
        public IQueryable<T> FindByCondation(Expression<Func<T,bool>>expression, bool AsTracking);

    }
}