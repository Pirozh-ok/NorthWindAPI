using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        /*[HttpGet]
        public IActionResult GetAllProduct([FromQuery] ProductPagination filter)
        {
            var validFilter = new QueryParameters()
            {
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize
            };

            var products = _productService.GetAllProducts(filter);

            if (filter.Sort == "desc")
                products = products.OrderByDescending(p=>p.ProductName);
            else products = products.OrderBy(p => p.ProductName);

            if (filter.Format == "xml")
            {
                return products is null || products.Count() == 0 ?
                    NotFound(Converter.ToXml(_notFoundMessage)) :
                 Ok(Converter.ToXml(products.ToList()));
            }

            return products is null || products.Count() == 0 ?
                 NotFound(Converter.ToJson(_notFoundMessage)) :
                 Ok(Converter.ToJson(products.ToList()));
        }*/
    }
}
