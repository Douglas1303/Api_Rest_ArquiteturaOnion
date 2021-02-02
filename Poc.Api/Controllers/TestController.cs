using Microsoft.AspNetCore.Mvc;

namespace Poc.Api.Controllers
{
    public class TestController : BaseController
    {
        public TestController()
        {
        }

        [HttpGet]
        public string GetTest()
        {
            return "Olá!";
        }
    }
}