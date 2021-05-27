using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private static readonly string[] Books = new[]
        {
            "Feluda somogro", "Hasuli baker upokotha"
        };

        [HttpGet]
        public string[] Get()
        {
            return Books;
        }
    }
}
