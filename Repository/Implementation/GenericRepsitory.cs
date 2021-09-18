using System;
using System.Linq;
using System.Linq.Expressions;
using Contracts.Interface;
using Entity.Context;
using Microsoft.EntityFrameworkCore;

namespace Repository.Implementation
{
            public class GenericRepository<T> : IGenericRepository<T> where T : class
            {
                        private readonly RepoDbContext _context;
                        private readonly DbSet<T> _Entity;

                        public GenericRepository(RepoDbContext context)
                        {
                            _context=context;
                            _Entity=context.Set<T>();
                        }
                        public void Create(T Model)
                        {
                                    _Entity.Add(Model);
                        }

                        public void Delete(T Model)
                        {
                                   _Entity.Remove(Model);
                        }

                        public IQueryable<T> FindAll(bool AsTracking)
                        {
                                return    !AsTracking?_Entity.AsNoTracking<T>():_Entity;
                        }

                        public IQueryable<T> FindByCondation(Expression<Func<T, bool>> expression, bool AsTracking)
                        {
                                   return !AsTracking? _Entity.Where(expression).AsNoTracking<T>():_Entity.Where((expression));
                        }

                        public void Update(T Model)
                        {
                                  _Entity.Update(Model);
                        }
            }
}