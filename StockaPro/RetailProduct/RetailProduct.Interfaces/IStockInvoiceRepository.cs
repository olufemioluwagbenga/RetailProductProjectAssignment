using RetailProduct.Models;
using System;
using System.Collections.Generic;

namespace RetailProduct.Interfaces
{
    public interface IStockInvoiceRepository
    {
        IEnumerable<StockInvoice> GetStockInvoices();
        StockInvoice GetStockInvoiceByID(int stockInvoice);
        StockInvoice GetStockInvoiceByCategory(int categoryId);
        StockInvoice GetStockInvoiceByUserId(int userId);
        void InsertStockInvoice(StockInvoice stockInvoice);
        void DeleteStockInvoice(int stockInvoiceId);
        void UpdateStockInvoice(StockInvoice stockInvoice);
        void Save();
    }
}
