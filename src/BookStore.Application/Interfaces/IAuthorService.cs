using BookStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Interfaces
{
    public interface IAuthorService : IDisposable
    {
        Task<IEnumerable<Author>> GetAll();
        Task<Author> GetById(int id);
        Task<Author> Add(Author author);
        Task<Author> Update(Author author);
        Task<bool> Remove(Author author);
        Task<IEnumerable<Author>> Search(string name);
    }
}
