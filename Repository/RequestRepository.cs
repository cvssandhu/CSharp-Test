
using IRepository;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace Repository
{
    public class RequestRepository: IRequestRepository, IDisposable
    {

        private readonly ICommonRepository _commonRespository;  
        private ApplicationDbContext context;

        public static bool UseRawSql = true;
        public RequestRepository(ApplicationDbContext context, ICommonRepository commonRespository)
        {
            this.context = context;
            _commonRespository = commonRespository;         
        }

        public async Task<Request> CreateRequest(Request request)
        {

            if (!UseRawSql)
                return Create(request);

            else
                return await CreateRequestUsingRawSql(request);
        }

        private Request Create(Request request)
        {
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var query=context.Request.Add(request);                
                context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
            return request;
        }

        private async Task<Request> CreateRequestUsingRawSql(Request request)
        {
            string commandText = "INSERT INTO devtest.Request (Timestamp, Kind) VALUES (@Timestamp, @Kind)";
                    SqlParameter[] sp = new SqlParameter[] {
                    new SqlParameter("@Timestamp", request.Timestamp),
                    new SqlParameter("@Kind", request.Kind.ToString())
                };
            
            int returnResult = await DataAccess.SqlHelper.ExecuteNonQueryAsync(_commonRespository.GetConnectionstring(), commandText,sp);

            request.Rid = returnResult >0 ? returnResult : Convert.ToDateTime(request.Timestamp).Ticks;
                
            return request;
        }
        public void DeleteRequest(Request request)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Request> GetAllRequest(bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Request> GetByIds(IEnumerable<Guid> ids, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public Request GetRequest(Guid requestId, bool trackChanges)
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