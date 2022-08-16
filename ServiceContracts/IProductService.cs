using Entities;

namespace ServiceContracts
{
    public interface IProductService
    {
        Task<Root> SearchProducts(SearchProductRequestDTO searchProductRequestDTO);
    }
}