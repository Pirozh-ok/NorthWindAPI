using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NorthWindAPI.Models;
using Microsoft.AspNetCore.Mvc.Abstractions;
using System.Text.Json;
using NorthWindAPI.Services.Interfaces;
using Newtonsoft.Json;

namespace NorthWindAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var customers = _customerService.GetAllCustomers();

            if(customers is null || customers.Count() == 0)
                return NotFound(JsonConvert.SerializeObject("Ничего не найдено",Formatting.Indented));

            return Ok(JsonConvert.SerializeObject(customers, Formatting.Indented));
        }

        [HttpGet("{id}")]
        public string GetById(string id)
        {
            /*using var context = new northwindContext();
            var customer = context.Customers.SingleOrDefault(c => c.CustomerId == id);
            return customer is null ? JsonSerializer.Serialize("Нет результатов") : JsonSerializer.Serialize(customer); */
            return null;
        }

        [HttpGet]
        public List<Customer> GetName([FromQuery] string company, [FromQuery] string? a)
        {
            /*using var context = new northwindContext();
            var customers = context.Customers.Where(c=> c.CompanyName == company).ToList();
            return customers.Count() > 0 ? customers : null;*/
            return null;
        }
    }
}
