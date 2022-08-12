using Entities;
namespace IRepository
{
    public interface IRequestRepository : IDisposable
    {
        IEnumerable<Request> GetAllRequest(bool trackChanges);
        Request GetRequest(Guid requestId, bool trackChanges);
        Task<Request> CreateRequest(Request request);
        IEnumerable<Request> GetByIds(IEnumerable<Guid> ids, bool trackChanges);
        void DeleteRequest(Request request);
    }
}