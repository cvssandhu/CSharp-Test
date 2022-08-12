
using IRepository;
using Entities;
namespace Repository
{
        public class RepositoryManager : IRepositoryManager
        {
            private ApplicationDbContext _repositoryContext;
            private IProductRepository _productRepository;
            private ICommonRepository _commonRepository;

        public RepositoryManager(ApplicationDbContext repositoryContext, ICommonRepository commonRepository)
        {
            _repositoryContext = repositoryContext;
            _commonRepository = commonRepository;
        }

        public IProductRepository Product
            {
                get
                {
                    if (_productRepository == null)
                    _productRepository = new ProductRepository(_repositoryContext, _commonRepository);

                    return _productRepository;
                }
            }
            public void Save() => _repositoryContext.SaveChanges();
        }
    
}