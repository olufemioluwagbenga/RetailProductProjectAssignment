using System;

namespace RetailProduct.Models
{
    public class StockInvoice
    {
        public int Id { get; set; }
        public int StockId { get; set; }
        public string StockName { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
    }
}
