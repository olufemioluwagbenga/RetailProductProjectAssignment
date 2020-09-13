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
    public class StockInvoiceRepository : IStockInvoiceRepository
    {
        private readonly StockContext _dbContext;

        public StockInvoiceRepository(StockContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void DeleteStockInvoice(int stockInvoiceId)
        {
            var stockInvoice = _dbContext.StockInvoices.Find(stockInvoiceId);
            _dbContext.StockInvoices.Remove(stockInvoice);
            Save();
        }

        public StockInvoice GetStockInvoiceByID(int stockInvoiceId)
        {
            return _dbContext.StockInvoices.Find(stockInvoiceId);
        }

        public StockInvoice GetStockInvoiceByCategory(int categoryId)
        {
            return _dbContext.StockInvoices.Find(categoryId);
        }

        public StockInvoice GetStockInvoiceByUserId(int userId)
        {
            return _dbContext.StockInvoices.Find(userId);
        }

        public IEnumerable<StockInvoice> GetStockInvoices()
        {
            return _dbContext.StockInvoices.ToList();
        }

        public void InsertStockInvoice(StockInvoice stockInvoice)
        {
            _dbContext.Add(stockInvoice);
            Save();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void UpdateStockInvoice(StockInvoice stockInvoice)
        {
            _dbContext.Entry(stockInvoice).State = EntityState.Modified;
            Save();
        }
    }
}
