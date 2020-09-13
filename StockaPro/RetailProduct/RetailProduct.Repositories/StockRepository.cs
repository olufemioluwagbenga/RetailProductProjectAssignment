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
    public class StockRepository : IStockRepository
    {
        private readonly StockContext _dbContext;

        public StockRepository(StockContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void DeleteStock(int stockId)
        {
            var stock = _dbContext.Stocks.Find(stockId);
            _dbContext.Stocks.Remove(stock);
            Save();
        }

        public Stock GetStockByID(int stockId)
        {
            return _dbContext.Stocks.Find(stockId);
        }

        public IEnumerable<Stock> GetStocks()
        {
            return _dbContext.Stocks.ToList();
        }

        public void InsertStock(Stock stock)
        {
            _dbContext.Add(stock);
            Save();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void UpdateStock(Stock stock)
        {
            _dbContext.Entry(stock).State = EntityState.Modified;
            Save();
        }
    }
}
