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
        private string _notFoundMessage = "Ничего не найдено";
        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET api/customers
        [HttpGet]
        public IActionResult GetAllCustomers([FromQuery] CustomerPagination filter)
        {
            var validFilter = new PaginationFilter()
            {
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize
            };

            var customers = _customerService.GetAllCustomers(filter);

            if (filter.Sort == "desc")
                customers = customers.OrderByDescending(c => c.CompanyName);
            else customers = customers.OrderBy(c => c.CompanyName);

            if (filter.Format == "xml")
            {
                return customers is null || customers.Count() == 0 ?
                    NotFound(Converter.ToXml(_notFoundMessage)):
                 Ok(Converter.ToXml(customers.ToList()));
            }

            return customers is null || customers.Count() == 0 ?
                 NotFound(Converter.ToJson(_notFoundMessage)) :
                 Ok(Converter.ToJson(customers.ToList()));
        }

        // GET api/customers/anton
        [HttpGet("{id}")]
        public IActionResult GetById(string id, [FromQuery] CustomerPagination filter)
        {
            var customer = _customerService.GetCustomerById(id);

            if (filter.Format == "xml")
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
        public IActionResult GetByName(string name, [FromQuery] CustomerPagination filter)
        {
            var customer = _customerService.GetCustomerByName(name);

            if (filter.Format == "xml")
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
        public IActionResult GetGeneralByName(string name, [FromQuery] CustomerPagination filter)
        {
            var customer = _customerService.GetGeneralInfoCustomerByName(name);

            if (filter.Format == "xml")
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
        public IActionResult GetGeneralById(string id, [FromQuery] CustomerPagination filter)
        {
            var customer = _customerService.GetGeneralInfoCustomerById(id);

            if (filter.Format == "xml")
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
        public IActionResult GetOrdersByCustomerId(string id, [FromQuery] CustomerPagination filter)
        {
            var orders = _customerService.GetOrdersByCustomerId(id);

            if (filter.Sort == "desc")
                orders = orders.OrderByDescending(o => o.OrderDate);

            if (filter.Format == "xml")
            {
                return orders is null || orders.Count() < 1  ?
                    NotFound(Converter.ToXml(_notFoundMessage)) :
                 Ok(Converter.ToXml(orders));
            }

            return orders is null || orders.Count() < 1 ?
                NotFound(Converter.ToJson(_notFoundMessage)) :
                Ok(Converter.ToJson(orders));
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