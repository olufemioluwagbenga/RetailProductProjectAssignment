using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RetailProduct.Interfaces;
using RetailProduct.Models;

namespace RetailProduct.API.Controllers
{
    [Route("api/StockInvoice")]
    [ApiController]
    public class StockInvoiceController : ControllerBase
    {

        private readonly IStockInvoiceRepository _stockInvoiceRepository;

        public StockInvoiceController(IStockInvoiceRepository stockInvoiceRepository)
        {
            _stockInvoiceRepository = stockInvoiceRepository;
        }
        //This is serve as purchase history
        [HttpGet]
        public IActionResult Get()
        {
            var stockInvoices = _stockInvoiceRepository.GetStockInvoices();
            return new OkObjectResult(stockInvoices);
        }

        //This is serve as purchase history by invoice 
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var stockInvoice = _stockInvoiceRepository.GetStockInvoiceByID(id);
            return new OkObjectResult(stockInvoice);
        }


        //This is serve as purchase history by category
        [HttpGet("{categoryId}")]
        public IActionResult GetStockInvoiceByCategory(int categoryId)
        {
            var stockInvoice = _stockInvoiceRepository.GetStockInvoiceByCategory(categoryId);
            return new OkObjectResult(stockInvoice);
        }


        //This is serve as purchase history by user
        [HttpGet("{userId}")]
        public IActionResult GetStockInvoiceByUserId(int userId)
        {
            var stockInvoice = _stockInvoiceRepository.GetStockInvoiceByUserId(userId);
            return new OkObjectResult(stockInvoice);
        }

        // POST: api/StockInvoice
        //buying of stocks 
        [HttpPost]
        public IActionResult Post([FromBody] StockInvoice stockInvoice)
        {
            using (var scope = new TransactionScope())
            {
                _stockInvoiceRepository.InsertStockInvoice(stockInvoice);
                scope.Complete();
                return CreatedAtAction(nameof(Get), new { id = stockInvoice.Id }, stockInvoice);
            }
        }

        // PUT: api/StockInvoice/5
        [HttpPut]
        public IActionResult Put([FromBody] StockInvoice stockInvoice)
        {
            if (stockInvoice != null)
            {
                using (var scope = new TransactionScope())
                {
                    _stockInvoiceRepository.UpdateStockInvoice(stockInvoice);
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _stockInvoiceRepository.DeleteStockInvoice(id);
            return new OkResult();
        }
    }
}
