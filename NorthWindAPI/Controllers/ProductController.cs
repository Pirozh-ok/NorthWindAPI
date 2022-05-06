using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NorthWindAPI.Models;
using NorthWindAPI.Paginations;
using NorthWindAPI.Services.Interfaces;

namespace NorthWindAPI.Controllers
{
    [Route("api/products/")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private string _notFoundMessage = "Ничего не найдено";
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET api/products
        [HttpGet]
        public IActionResult GetAllProduct([FromQuery] ProductParameters filter)
        {
            filter.Collection = _productService.GetAllProducts();
            string response = filter.ResultProcessing();
            return filter.IsSuccess ? Ok(response) : NotFound(_notFoundMessage);
        }

        // GET api/products/1
        [HttpGet("{id}")]
        public IActionResult GetProductById(int id, [FromQuery] string? format)
        {
            try
            {
                var product = _productService.GetProductById(id);
                return format == "xml" ? Ok(Converter.ToXml(product)) : Ok(Converter.ToJson(product));
            }
            catch (Exception ex)
            {
                return format == "xml" ? NotFound(Converter.ToXml(_notFoundMessage)) : NotFound(Converter.ToJson(_notFoundMessage));
            }
        }

        // GET api/products/name/Chai
        [HttpGet("name/{name}")]
        public IActionResult GetProductByName(string name, [FromQuery] string? format)
        {
            try
            {
                var product = _productService.GetProductByName(name);
                return format == "xml" ? Ok(Converter.ToXml(product)) : Ok(Converter.ToJson(product));
            }
            catch (Exception ex)
            {
                return format == "xml" ? NotFound(Converter.ToXml(_notFoundMessage)) : NotFound(Converter.ToJson(_notFoundMessage));
            }
        }

        // POST api/products/
        [HttpPost]
        public IActionResult CreateCustomer([FromBody] Product product)
        {
            try
            {
                if (product is null)
                    return BadRequest("Передано пустое значение");

                _productService.CreateProduct(product);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/products/
        [HttpPut]
        public IActionResult UpdateCustomer([FromBody] Product product)
        {
            try
            {
                if (product is null)
                    return BadRequest("Передано пустое значение");

                _productService.UpdateProduct(product);
                return StatusCode(204);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/product
        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            try
            {
                _productService.DeleteProduct(id);
                return StatusCode(202);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("pricelist/")]
        public IActionResult GetPriceList([FromQuery] string? format)
        {        
            try
            {
                var priceList = _productService.GetPriceList();
                return format == "xml" ? Ok(Converter.ToXml(priceList)) : Ok(Converter.ToJson(priceList));
            }
            catch (Exception ex)
            {
                return format == "xml" ? NotFound(Converter.ToXml(_notFoundMessage)) : NotFound(Converter.ToJson(_notFoundMessage));
            }
        }

        // GET api/products/stat
        [HttpGet("stat")]
        public IActionResult GetStat([FromQuery] int? maxYear, [FromQuery] int? minYear, [FromQuery] string? format)
        {
            var maxY = maxYear is null ? 3000 : (int)maxYear;
            var minY = minYear is null ? 0 : (int)minYear;
            try
            {
                var statistic = _productService.GetSalesStatistics(maxY, minY);
                return format == "xml"? Ok(Converter.ToXml(statistic)) : Ok(Converter.ToJson(statistic));
            }
            catch
            {
                return NotFound(_notFoundMessage);
            }
        }
    }
}
