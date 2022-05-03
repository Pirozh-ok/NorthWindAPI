using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NorthWindAPI.Models;
using NorthWindAPI.Paginations;
using NorthWindAPI.Services.Interfaces;

namespace NorthWindAPI.Controllers
{
    [Route("api/customers/")]
    [ApiController]
    public class CustomersController: ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly string _notFoundMessage = "Ничего не найдено";
        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET api/customers
        [HttpGet]
        public IActionResult GetAllCustomers([FromQuery] CustomerParameters filter)
        {
            filter.Collection = _customerService.GetAllCustomers();
            string response = filter.ResultProcessing();
            return filter.IsSuccess? Ok(response) : NotFound(response);
        }

        // GET api/customers/anton
        [HttpGet("{id}")]
        public IActionResult GetById(string id, [FromQuery] string? format)
        {
            var customer = _customerService.GetCustomerById(id);

            if (format == "xml")
            {
                return customer is null ?
                    NotFound(Converter.ToXml(_notFoundMessage)) :
                 Ok(Converter.ToXml(customer));
            }

            return customer is null ?
                NotFound(Converter.ToJson(_notFoundMessage)):
                Ok(Converter.ToJson(customer));
        }
        
        // GET api/customers/name/Antonio Moreno
        [HttpGet("name/{name}")]
        public IActionResult GetByName(string name, [FromQuery] string? format)
        {
            var customer = _customerService.GetCustomerByName(name);

            if (format == "xml")
            {
                return customer is null ?
                    NotFound(Converter.ToXml(_notFoundMessage)) :
                 Ok(Converter.ToXml(customer));
            }

            return customer is null ?
                NotFound(Converter.ToJson(_notFoundMessage)) :
                Ok(Converter.ToJson(customer));
        }

        // GET api/customers/general/name/Around the Horn
        [HttpGet("general/name/{name}")]
        public IActionResult GetGeneralByName(string name, [FromQuery] string? format)
        {
            var customer = _customerService.GetGeneralInfoCustomerByName(name);

            if (format == "xml")
            {
                return customer is null ?
                    NotFound(Converter.ToXml(_notFoundMessage)) :
                 Ok(Converter.ToXml(customer));
            }

            return customer is null ?
                NotFound(Converter.ToJson(_notFoundMessage)) :
                Ok(Converter.ToJson(customer));
        }

        // GET api/customers/general/arout
        [HttpGet("general/{id}")]
        public IActionResult GetGeneralById(string id, [FromQuery] string? format)
        {
            var customer = _customerService.GetGeneralInfoCustomerById(id);

            if (format == "xml")
            {
                return customer is null ?
                    NotFound(Converter.ToXml(_notFoundMessage)) :
                 Ok(Converter.ToXml(customer));
            }

            return customer is null ?
                NotFound(Converter.ToJson(_notFoundMessage)) :
                Ok(Converter.ToJson(customer));
        }

        // GET api/customers/VANYA/orders
        [HttpGet("{id}/orders")]
        public IActionResult GetOrdersByCustomerId(string id, [FromQuery] OrderParameters filter)
        {
            filter.Collection = _customerService.GetOrdersByCustomerId(id);
            string response = filter.ResultProcessing();
            return filter.IsSuccess? Ok(response) : NotFound(response);
        }

        // POST api/customers/
        [HttpPost]
        public IActionResult CreateCustomer([FromBody] Customer customer)
        {
            try
            {
                if (customer is null)
                    BadRequest("Передано пустое значение");

                _customerService.CreateCustomer(customer);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/customers/
        [HttpPut]
        public IActionResult UpdateCustomer([FromBody] Customer customer)
        {
            try
            {
                if (customer is null)
                    BadRequest("Передано пустое значение");

                _customerService.UpdateCustomer(customer);
                return StatusCode(204);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/customers/VANYA
        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(string id)
        {
            try
            {
                _customerService.DeleteCustomer(id);
                return StatusCode(202);
            }
            catch (Exception ex)
            {
                return NotFound(JsonConvert.SerializeObject(ex.Message));
            }
        }
    }
}