﻿using Myshop.DAL.Data;
using MyShop.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using MyShop.Domain.Repositories.Contract;

namespace MyShop.DAL.Repositories.Implementation
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task UpdateAsync(Product product)
        {
            var ProductInDb = await _context.Products.FirstOrDefaultAsync(x => x.Id == product.Id);
            if (ProductInDb != null)
            {
                ProductInDb.Name = product.Name;
                ProductInDb.Description = product.Description;
                ProductInDb.Price = product.Price;
                ProductInDb.Img = product.Img;
                ProductInDb.CategoryId = product.CategoryId;

                ProductInDb.IsFeatured = product.IsFeatured;

                _context.Products.Update(ProductInDb);
            }
        }

    }
}
