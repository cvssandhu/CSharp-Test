using IRepository;
using ServiceContracts;
using Entities;
using System.Text.Json;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UserService(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }

        public async Task<User> GetUser(string UniqueMachineId)
        {
            return await GetUserAsync(UniqueMachineId);

        }


        private async Task<User> GetUserAsync(string queryString)
        {
            if (queryString != String.Empty)
            {
                var _httpClient = this._httpClientFactory.CreateClient(name: "twg.azure-api.net");
                if (_httpClient != null)
                {
                    string Url = $"bolt/newuser.json?{queryString}";
                    HttpResponseMessage searchResponse = await _httpClient.GetAsync(requestUri: Url);
                    if (searchResponse.IsSuccessStatusCode)
                    {
                        string searchResult =  searchResponse.Content.ReadAsStringAsync().Result;
                        
                            User? userInfo = JsonSerializer.Deserialize<User>(searchResult);
                            return userInfo;
                        
                    }
                    else
                    {
                        throw new Exception("Get User Async" + Environment.NewLine + searchResponse.ReasonPhrase);
                    }
                }
            }
            return null;
        }
    }
}