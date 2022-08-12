
using IRepository;
using Entities;
using System.Linq.Expressions;

namespace Repository
{
        public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
        {
            protected ApplicationDbContext _applicationDbContext;

            public RepositoryBase(ApplicationDbContext repositoryContext)
            {
                _applicationDbContext = repositoryContext;
            }

        public void Create(T entity)
        {
             _applicationDbContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> FindAll(bool trackChanges){

            return _applicationDbContext.Set<T>();
               
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}