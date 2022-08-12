using ServiceContracts;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace CSharpTest.Controllers {
    [ApiController]
    [Route ("[controller]")]
    public class UserController : ControllerBase {
        private readonly IUserService _userService;
        private IConfiguration configuration;

        public UserController(IConfiguration iconfig, IUserService userService) {
            configuration = iconfig;
            _userService = userService;

        }
       
        [Route ("newuser"), HttpGet]
        public async Task<ActionResult> GetUserAsync() {

            var user = await this._userService.GetUser(Environment.MachineName);            
            return new OkObjectResult (user);
        }
    }
}
