using IRepository;
using Microsoft.Extensions.Configuration;

namespace Repository
{
    public class CommonRepository : ICommonRepository
    {
        private readonly IConfiguration _configuration;
        public CommonRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GetConnectionstring()
        {
            return _configuration.GetConnectionString("DB");
        }
    }
}
