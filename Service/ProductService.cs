using IRepository;
using ServiceContracts;
using Entities;
using System.Text.Json;

namespace Services
{
    public class ProductService: IProductService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly IRequestRepository _requestRepository;
        private readonly ISearchRequestRepository _searchRequestRepository;
        private readonly IProductRepository _productRepository;


        public const int ProductsCountToSave = 3;
        public ProductService(IHttpClientFactory httpClientFactory, IRequestRepository requestRepository, 
            ISearchRequestRepository searchRequestRepository, IProductRepository productRepository)
        {
            this._httpClientFactory = httpClientFactory;

            this._requestRepository = requestRepository;
            this._searchRequestRepository = searchRequestRepository;
            this._productRepository = productRepository;
            
        }

        public async Task<Root> SearchProducts(SearchProductRequestDTO searchProductRequestDTO)
        {

            var searchResults =  await SearchProductsAsync(searchProductRequestDTO.Branch,
                searchProductRequestDTO.Search,
                searchProductRequestDTO.Screen,
                searchProductRequestDTO.StartAt,
                searchProductRequestDTO.Limit);
            if (searchResults != null)
            {
                //lstProducts.Results.for(o => o.Products.ToList().Take(3));
                //List<Result>? result = searchResults.Results.Take(3).ToList();
                LogRequestResponse(searchProductRequestDTO.Search, searchResults);
               // LogRequestResponse(Search, searchResults);
                //SaveProducts(result[0].Products.Take(3).ToList());
            }
             return searchResults;
        }

        private async Task<Root> SearchProductsAsync(string Branch, string Search, string Screen, string StartAt, string Limit)
        {
                var _httpClient = this._httpClientFactory.CreateClient(name: "twg.azure-api.net");
                if (_httpClient != null)
                {
                    string Url = $"bolt/search.json?UserId=21E3BC8B-CA74-4C9A-9A0F-F0748A550B92&Search={Search}&Branch={Branch}&Screen={Screen}&Start={StartAt}&Limit={Limit}";
                    HttpResponseMessage searchResponse = await _httpClient.GetAsync(requestUri: Url);
                    if (searchResponse.IsSuccessStatusCode)
                    {
                         string jsonResult = searchResponse.Content.ReadAsStringAsync().Result;

                        Root? searchResult = JsonSerializer.Deserialize<Root>(jsonResult); ;

                        return searchResult;

                    }
                else
                {
                    throw new Exception("Get Product Async" + Environment.NewLine + searchResponse.ReasonPhrase);
                }
            }
            
            return null;
        }

        private async void LogRequestResponse(string search, Root results)
        {
            Request request = await CreateRequest(new Request { Timestamp = DateTime.Now, Kind = 'R' });

            if (request.Rid > 0)
            {
                CreateSearchRequest(new SearchRequest()
                {
                    Hits = Convert.ToInt32(results.HitCount),
                    Rid = request.Rid,
                    Search = search,
                    SuccessInd = char.Parse(results.Found)
                });


                List<SearchTopProducts> searchTopProducts = FindTopProducts(results, request.Rid);

                this._productRepository.CreateProduct(searchTopProducts);
            }
           
        }

        private async Task<Request> CreateRequest(Request request)
        {
             return await _requestRepository.CreateRequest(request);
        }

        private void CreateSearchRequest(SearchRequest searchRequest)
        {
            _searchRequestRepository.CreateSearchRequest(searchRequest);
        }

        private void CreateProduct(List<SearchTopProducts> lstSearchTopProducts)
        {
            _productRepository.CreateProduct(lstSearchTopProducts);
        }

        public List<SearchTopProducts> FindTopProducts(Root? searchResults, Int64 nRid)
        {
            List<SearchTopProducts> lstSearchTopProducts = searchResults.Results.SelectMany(s => s.Products ?? new List<Product>())
                .Where(w => w.ProductKey != null).Select(s => new SearchTopProducts()
                {
                    Rid = nRid,
                    Order = Convert.ToInt32(s.Class0ID),
                    ProductId = s.ProductKey,
                }).Take(ProductsCountToSave).ToList();
            
            
            return lstSearchTopProducts;
        }
    }
}