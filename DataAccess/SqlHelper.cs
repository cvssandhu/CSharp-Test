
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;
using Microsoft.Extensions.Configuration;
using System.Transactions;

namespace DataAccess
{
    public static class SqlHelper
    {

        public static async Task<int> ExecuteNonQueryAsync(string connectionString, string sql, SqlParameter[] p)
        {
            SqlTransaction sTran = null;
            int retval = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        
                        sTran = conn.BeginTransaction("trans");

                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Transaction = sTran;
                        cmd.CommandType = CommandType.Text;
                        if (p != null)
                        {
                            for (int i = 0; i < p.Length; i++)
                            {
                                cmd.Parameters.Add(p[i]);
                            }
                        }
                        retval =  (int)cmd.ExecuteNonQuery();
                        
                        sTran.Commit();

                       
                        scope.Complete(); 
                        conn.Close();
                        conn.Dispose();
                    }
                }
            }
            catch
            {
                if (sTran != null)
                {
                    sTran.Rollback();
                }

            }
            return retval;
        }

    }

}