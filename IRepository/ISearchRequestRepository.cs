using Entities;
namespace IRepository
{
    public interface ISearchRequestRepository : IDisposable
    {
        IEnumerable<SearchRequest> GetAllSearchRequest(bool trackChanges);
        SearchRequest GetSearchRequest(Guid productId, bool trackChanges);
        void CreateSearchRequest(SearchRequest searchRequest);
        IEnumerable<SearchRequest> GetByIds(IEnumerable<Guid> ids, bool trackChanges);
        void DeleteSearchRequest(SearchRequest searchRequest);
    }
}