using ServiceContracts;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using CSharpTest.Model;

namespace CSharpTest.Controllers {
    [ApiController]
    [Route ("[controller]")]
    public class BillingController : ControllerBase {
        private readonly IBillingService _billingService;
        private IConfiguration configuration;

        public BillingController(IConfiguration iconfig, IBillingService billingService) {
            configuration = iconfig;
            _billingService = billingService;
        }
       

        [Route ("Price"), HttpGet]
        public async Task<ActionResult> Price([FromQuery] SearchProductPriceModel searchProductPriceModel) {

            var priceInfo = await _billingService.GetPriceInfoAsync(searchProductPriceModel.Branch, searchProductPriceModel.Barcode);

            return new OkObjectResult(priceInfo);
        }
    }
}
