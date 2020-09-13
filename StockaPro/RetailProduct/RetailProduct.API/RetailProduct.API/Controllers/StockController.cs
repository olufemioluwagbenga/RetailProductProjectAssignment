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
    [Route("api/Stock")]
    [ApiController]
    public class StockController : ControllerBase
    {

        private readonly IStockRepository _stockRepository;

        public StockController(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }
        // GET: api/Stock
        [HttpGet]
        public IActionResult Get()
        {
            var stocks = _stockRepository.GetStocks();
            return new OkObjectResult(stocks);
        }

        // GET: api/Stock/5
        //[HttpGet("{id}", Name = "Get")]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var stock = _stockRepository.GetStockByID(id);
            return new OkObjectResult(stock);
        }

        // POST: api/Stock
        [HttpPost]
        public IActionResult Post([FromBody] Stock stock)
        {
            using (var scope = new TransactionScope())
            {
                _stockRepository.InsertStock(stock);
                scope.Complete();
                return CreatedAtAction(nameof(Get), new { id = stock.Id }, stock);
            }
        }

        // PUT: api/Stock/5
        [HttpPut]
        public IActionResult Put([FromBody] Stock stock)
        {
            if (stock != null)
            {
                using (var scope = new TransactionScope())
                {
                    _stockRepository.UpdateStock(stock);
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
            _stockRepository.DeleteStock(id);
            return new OkResult();
        }
    }
}
