using ServiceContracts;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using CSharpTest.Model;
using Entities;

namespace CSharpTest.Controllers {
    [ApiController]
    [Route ("[controller]")]
    public class ProductController : ControllerBase {
        private readonly IProductService _productService;

        public ProductController(IProductService productService) {
           _productService = productService;
        }
       

        [Route ("Search"), HttpGet]
        public async Task<ActionResult> SearchAsync([FromQuery] SearchProductModel SearchProductModel) {

            var searchResults = await this._productService.SearchProducts(SearchProductModel.Branch, SearchProductModel.Search, SearchProductModel.Screen, SearchProductModel.StartAt, SearchProductModel.Limit);            
            return new OkObjectResult (searchResults);
        }

    }
}
