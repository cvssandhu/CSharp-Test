using Entities;
namespace IRepository
{
    public interface IProductRepository: IDisposable
    {
        IEnumerable<SearchTopProducts> GetAllProducts(bool trackChanges);
        SearchTopProducts GetProduct(Guid productId, bool trackChanges);
        void CreateProduct(List<SearchTopProducts> product);
        IEnumerable<SearchTopProducts> GetByIds(IEnumerable<Guid> ids, bool trackChanges);
        void DeleteProduct(SearchTopProducts product);
    }
}