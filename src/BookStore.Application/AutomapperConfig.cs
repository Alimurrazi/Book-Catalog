using AutoMapper;
using BookStore.Domain.Models;

namespace BookStore.Application.Configuration
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<Category, Dtos.Category.CategoryAddDto>().ReverseMap();
            CreateMap<Category, Dtos.Category.CategoryEditDto>().ReverseMap();
            CreateMap<Category, Dtos.Category.CategoryResultDto>().ReverseMap();
            CreateMap<Book, Dtos.Book.BookAddDto>().ReverseMap();
            CreateMap<Book, Dtos.Book.BookEditDto>().ReverseMap();
            CreateMap<Book, Dtos.Book.BookResultDto>().ReverseMap();
        }
    }
}