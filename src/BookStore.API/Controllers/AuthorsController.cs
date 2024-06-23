using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers
{
    [Route("api/[controller]")]
    public class AuthorsController : MainController
    {
        [HttpGet]
        public ActionResult<string> Index()
        {
            return Ok("hello");
        }
    }
}
