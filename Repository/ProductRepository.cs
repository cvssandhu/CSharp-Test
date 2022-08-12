
using IRepository;
using Entities;
using Microsoft.EntityFrameworkCore;
using System.Transactions;
using Microsoft.Data.SqlClient;

namespace Repository
{
    public class ProductRepository : IProductRepository, IDisposable
    {
        private readonly ICommonRepository _commonRespository;
        private ApplicationDbContext context;

        public static bool UseRawSql = true;
        public ProductRepository(ApplicationDbContext context, ICommonRepository commonRespository)
        {
            this.context = context;
            _commonRespository = commonRespository;
        }

        public void CreateProduct(List<SearchTopProducts> lstSearchTopProducts)
        {
            if (!UseRawSql)
                CreateProductAsync(lstSearchTopProducts);
            else
                CreateProductRawSql(lstSearchTopProducts);
            
        }

        private async void CreateProductAsync(List<SearchTopProducts> lstSearchTopProducts)
        {
            using var transaction = context.Database.BeginTransaction();

            try
            {
                context.SearchTopProducts.AddRange(lstSearchTopProducts);
                context.SaveChanges();

                transaction.Commit();
            }
            catch (Exception)
            {
                // If a failure occurred, we rollback to the savepoint and can continue the transaction
                transaction.Rollback();
            }
        }
        private async void CreateProductRawSql(List<SearchTopProducts> lstSearchTopProducts)
        {

            string commandText = "INSERT INTO devtest.SearchTopProducts (Rid, Order, ProductId)" +
                                 " VALUES (@Rid, @Order, @ProductId)";

            for (int i = 0; i < lstSearchTopProducts.Count; i++)
            {
                SearchTopProducts searchTopProducts = lstSearchTopProducts[i];
                SqlParameter[] sp = new SqlParameter[] {
                    new SqlParameter("@Rid", searchTopProducts.Rid),
                    new SqlParameter("@Order", searchTopProducts.Order),
                    new SqlParameter("@ProductId", searchTopProducts.ProductId)
                };

                 await DataAccess.SqlHelper.ExecuteNonQueryAsync(_commonRespository.GetConnectionstring(), commandText, sp);
            }
        }

        public void DeleteProduct(SearchTopProducts product)
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
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        public IEnumerable<SearchTopProducts> GetAllProducts(bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SearchTopProducts> GetByIds(IEnumerable<Guid> ids, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public SearchTopProducts GetProduct(Guid productId, bool trackChanges)
        {
            throw new NotImplementedException();
        }

    }
}