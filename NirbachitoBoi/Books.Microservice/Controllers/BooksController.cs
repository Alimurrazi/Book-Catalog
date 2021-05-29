using MassTransit;
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
        private readonly IBus _bus;

        public BooksController(IBus bus)
        {
            _bus = bus;
        }

        private static readonly string[] Books = new[]
        {
            "Feluda somogro", "Hasuli baker upokotha"
        };

        [HttpGet]
        public async Task<IActionResult>Get()
        {
            Uri uri = new Uri("rabbitmq://localhost/books");
            Book book = new Book();
            book.Name = "keno";
            var endPoint = await _bus.GetSendEndpoint(uri);
            await endPoint.Send(book);
            return Ok(Books);
        }
    }

    public class Book
    {
        public string Name { get; set; }
    }
}
