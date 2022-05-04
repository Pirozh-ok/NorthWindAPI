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
            var product = _productService.GetProductById(id);

            if (format == "xml")
            {
                return product is null ?
                    NotFound(Converter.ToXml(_notFoundMessage)) :
                 Ok(Converter.ToXml(product));
            }

            return product is null ?
                NotFound(Converter.ToJson(_notFoundMessage)) :
                Ok(Converter.ToJson(product));
        }

        // GET api/products/name/Chai
        [HttpGet("name/{name}")]
        public IActionResult GetProductByName(string name, [FromQuery] string? format)
        {
            var product = _productService.GetProductByName(name);

            if (format == "xml")
            {
                return product is null ?
                    NotFound(Converter.ToXml(_notFoundMessage)) :
                 Ok(Converter.ToXml(product));
            }

            return product is null ?
                NotFound(Converter.ToJson(_notFoundMessage)) :
                Ok(Converter.ToJson(product));
        }

        // POST api/products/
        [HttpPost]
        public IActionResult CreateCustomer([FromBody] Product product)
        {
            try
            {
                if (product is null)
                    BadRequest("Передано пустое значение");

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
                    BadRequest("Передано пустое значение");

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
    }
}
