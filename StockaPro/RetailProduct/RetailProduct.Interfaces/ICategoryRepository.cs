using RetailProduct.Models;
using System;
using System.Collections.Generic;

namespace RetailProduct.Interfaces
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetCategorys();
        Category GetCategoryByID(int category);
        void InsertCategory(Category category);
        void DeleteCategory(int categoryId);
        void UpdateCategory(Category category);
        void Save();
    }
}
