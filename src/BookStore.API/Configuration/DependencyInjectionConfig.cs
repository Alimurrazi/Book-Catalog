﻿using BookStore.Application.Interfaces;
using BookStore.Application.Services;
using BookStore.Domain.Interfaces;
using BookStore.Domain.Services;
using BookStore.Infrastructure.Context;
using BookStore.Infrastructure.Repositories;
using BookStore.Infrastructure.Store;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<BookStoreDbContext>();

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IBookRepository, BookRepository>();

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IBookService, BookService>();

            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IAuthRepository, AuthRepository>();

            // added in-memory storage
            services.AddSingleton<InMemoryTokenStore>();

            return services;
        }
    }
}