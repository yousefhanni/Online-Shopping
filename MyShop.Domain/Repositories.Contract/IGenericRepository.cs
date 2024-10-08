﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyShop.Domain.Repositories.Contract
{
    public interface IGenericRepository<T> where T : class
    {
        // _Context.Categories.Include("Products").ToList();
        // _Context.Categories.Where(x => x.Id == id).ToList();
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null, string? includeProperties = null);

        // _Context.Categories.Include("Products").ToSingleOrDefault();
        // _Context.Categories.Where(x => x.Id == id).ToSingleOrDefault();
        Task<T> GetItemAsync(Expression<Func<T, bool>>? predicate = null, string? includeProperties = null);

        //_Context.Categories.Add(category);
        Task AddAsync(T entity);

		//_Context.Categories.Remove(category);
		Task Remove(T entity);

        Task RemoveRangeAsync(IEnumerable<T> entities);
    }
}
