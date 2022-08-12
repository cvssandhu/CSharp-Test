
using Entities;

namespace ServiceContracts
{
    public interface IBillingService
    {
        Task<PriceInfo> GetPriceInfoAsync(string branch, string barCode);
    }
}