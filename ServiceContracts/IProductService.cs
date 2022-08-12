using Entities;

namespace ServiceContracts
{
    public interface IProductService
    {
        Task<Root> SearchProducts(string Branch, string Search, string Screen, string StartAt, string Limit);
    }
}