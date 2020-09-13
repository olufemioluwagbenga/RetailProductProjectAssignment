using RetailProduct.Models;
using System;
using System.Collections.Generic;

namespace RetailProduct.Interfaces
{
    public interface IStockRepository
    {
        IEnumerable<Stock> GetStocks();
        Stock GetStockByID(int stock);
        void InsertStock(Stock stock);
        void DeleteStock(int stockId);
        void UpdateStock(Stock stock);
        void Save();
    }
}
