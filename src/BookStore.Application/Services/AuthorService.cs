using BookStore.Application.Interfaces;
using BookStore.Domain.Interfaces;
using BookStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<Author> Add(Author author)
        {
            var result = _authorRepository.Search(a => a.Name == author.Name).Result.Any();
            if(result)
            {
                return null;
            }
            await _authorRepository.Add(author);
            return author;
        }

        public void Dispose()
        {
            _authorRepository?.Dispose();
        }

        public async Task<IEnumerable<Author>> GetAll()
        {
            return await _authorRepository.GetAll();
        }

        public async Task<Author> GetById(int id)
        {
            return await _authorRepository.GetById(id);
        }

        public Task<bool> Remove(Author author)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Author>> Search(string name)
        {
            return await _authorRepository.Search(a => a.Name.Contains(name));
        }

        public async Task<Author> Update(Author author)
        {
            var result = _authorRepository.GetById(author.Id).Result;
            if(result is null)
            {
                return null;
            }
            await _authorRepository.Update(author);
            return author;
        }
    }
}
