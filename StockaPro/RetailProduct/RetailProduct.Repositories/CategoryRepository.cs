using Microsoft.EntityFrameworkCore;
using RetailProduct.Entities;
using RetailProduct.Interfaces;
using RetailProduct.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RetailProduct.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly StockContext _dbContext;

        public CategoryRepository(StockContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void DeleteCategory(int categoryId)
        {
            var category = _dbContext.Categories.Find(categoryId);
            _dbContext.Categories.Remove(category);
            Save();
        }

        public Category GetCategoryByID(int categoryId)
        {
            return _dbContext.Categories.Find(categoryId);
        }

        public IEnumerable<Category> GetCategorys()
        {
            return _dbContext.Categories.ToList();
        }

        public void InsertCategory(Category category)
        {
            _dbContext.Add(category);
            Save();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void UpdateCategory(Category category)
        {
            _dbContext.Entry(category).State = EntityState.Modified;
            Save();
        }
    }
}
