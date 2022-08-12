
using IRepository;
using Entities;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using Microsoft.Data.SqlClient;

namespace Repository
{
    public class SearchRequestRepository : ISearchRequestRepository, IDisposable
    {
        private readonly ICommonRepository _commonRespository;
        private ApplicationDbContext context;

        public static bool UseRawSql = true;
        public SearchRequestRepository(ApplicationDbContext context, ICommonRepository commonRespository)
        {
            this.context = context;
            _commonRespository = commonRespository;
        }

        public void CreateSearchRequest(SearchRequest searchRequest)
        {

            if (!UseRawSql)
                 Create(searchRequest);

            else
                 CreateRequestUsingRawSql(searchRequest);
        }

        private SearchRequest Create(SearchRequest searchRequest)
        {
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var query = context.SearchRequest.Add(searchRequest);
                context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
            return searchRequest;
        }

        private async Task<SearchRequest> CreateRequestUsingRawSql(SearchRequest searchRequest)
        {
                string commandText = "INSERT INTO devtest.SearchRequest (Rid, Hits, Search, SuccessInd)" +
                                     " VALUES (@Rid, @Hits, @Search, @SuccessInd)";

                SqlParameter[] sp = new SqlParameter[] {
                    new SqlParameter("@Rid", searchRequest.Rid),
                    new SqlParameter("@Hits", searchRequest.Hits),
                    new SqlParameter("@Search", searchRequest.Search),
                    new SqlParameter("@SuccessInd", searchRequest.SuccessInd.ToString())
                };

                int returnResult = await DataAccess.SqlHelper.ExecuteNonQueryAsync(_commonRespository.GetConnectionstring(), commandText, sp);
            return searchRequest;
        }

        public void DeleteSearchRequest(SearchRequest searchRequest)
        {
            throw new NotImplementedException();
        }
       

        public IEnumerable<SearchRequest> GetAllSearchRequest(bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SearchRequest> GetByIds(IEnumerable<Guid> ids, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public SearchRequest GetSearchRequest(Guid productId, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            context.Dispose();
            context = null;
        }
    }
}