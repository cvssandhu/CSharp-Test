using IRepository;
using ServiceContracts;
using Entities;
using System.Text.Json;
using System.Web;
using System.Collections.Specialized;

namespace Services
{
    public class BillingService : IBillingService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BillingService(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }

        public async Task<PriceInfo> GetPriceInfoAsync(string barCode, string branch)
        {
            var priceInfo = await GetPriceAsync(barCode, branch);

            return priceInfo;
        }


        private async Task<PriceInfo> GetPriceAsync(string branch, string barCode)
        {
            var _httpClient = this._httpClientFactory.CreateClient(name: "twg.azure-api.net");
            if (_httpClient != null)
            {
                HttpResponseMessage searchResponse = await _httpClient.GetAsync(requestUri: RequestUrl(GetRequestParameters(branch, barCode)));
                if (searchResponse.IsSuccessStatusCode)
                {
                    string searchResult = searchResponse.Content.ReadAsStringAsync().Result;

                    return SerializableJsonResult(searchResult);

                }
                else
                {
                    throw new Exception("Get Price Async" + Environment.NewLine + searchResponse.ReasonPhrase);
                }
            }
            return null;
        }

        private NameValueCollection GetRequestParameters(string branch, string barCode)
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // Request parameters
            queryString["Barcode"] = barCode;
            queryString["Branch"] = branch;

            //Other parameters for this request.
            //queryString["MachineID"] = string.Empty;
            //queryString["UserID"] = string.Empty;
            //queryString["DontSave"] = string.Empty;

            return queryString;
        }

        private string RequestUrl(NameValueCollection requestParameters)
        {
            return $"bolt/price.json?UserId=21E3BC8B-CA74-4C9A-9A0F-F0748A550B92&{requestParameters}";
        }

        private PriceInfo SerializableJsonResult(string searchResult)
        {
            PriceInfo? priceInfo = JsonSerializer.Deserialize<PriceInfo>(searchResult);

            if (priceInfo != null && priceInfo.Found == "N")
                priceInfo.ScanID = null;

            return priceInfo;
        }
    }
  
}