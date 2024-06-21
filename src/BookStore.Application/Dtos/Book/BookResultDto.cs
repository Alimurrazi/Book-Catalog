using System;

namespace BookStore.Application.Dtos.Book
{
    public class BookResultDto
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Author { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public double Value { get; set; }

        public DateTime PublishDate { get; set; }
    }
}