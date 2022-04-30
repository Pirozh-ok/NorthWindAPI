using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NorthWindAPI.Models;
using NorthWindAPI.Paginations;
using NorthWindAPI.Services.Interfaces;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace NorthWindAPI.Controllers
{
    [Route("api/customers/")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET api/customers
        [HttpGet]
        public IActionResult Get([FromQuery] CustomerPagination filter)
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

            /*XmlAttributeOverrides attrOverrides = new XmlAttributeOverrides();
            XmlAttributes attrs = new XmlAttributes { XmlIgnore = true };
            attrOverrides.Add(typeof(Customer), "Orders", attrs);

            XmlSerializer serializer = new XmlSerializer(typeof(List<Customer>));
            var stringwriter = new StringWriter();
            serializer.Serialize(stringwriter, customers);
            return Ok(stringwriter.ToString());*/

            return customers is null || customers.Count() == 0 ?
                 NotFound(JsonConvert.SerializeObject("Ничего не найдено",Formatting.Indented)):
                 Ok(JsonConvert.SerializeObject(customers, Formatting.Indented, new JsonSerializerSettings()
                 {
                     ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                 }));
        }

        // GET api/customers/anton
        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            var customer = _customerService.GetCustomerById(id);
            return customer is null ?
                NotFound(JsonConvert.SerializeObject("Ничего не найдено", Formatting.Indented)) :
                Ok(JsonConvert.SerializeObject(customer, Formatting.Indented));
        }

        // GET api/customers/name/Antonio Moreno
        [HttpGet("name/{name}")]
        public IActionResult GetByName(string name)
        {
            var customer = _customerService.GetCustomerByName(name);
            return customer is null ? 
                NotFound(JsonConvert.SerializeObject("Ничего не найдено", Formatting.Indented)):
                Ok(JsonConvert.SerializeObject(customer, Formatting.Indented));
        }

        // GET api/customers/general/name/Around the Horn
        [HttpGet("general/name/{name}")]
        public IActionResult GetGeneralByName(string name)
        {
            var customer = _customerService.GetGeneralInfoCustomerByName(name);
            return customer is null ?
                NotFound(JsonConvert.SerializeObject("Ничего не найдено", Formatting.Indented)) :
                Ok(JsonConvert.SerializeObject(customer, Formatting.Indented));
        }

        // GET api/customers/general/arout
        [HttpGet("general/{id}")]
        public IActionResult GetGeneralById(string id)
        {
            var customer = _customerService.GetGeneralInfoCustomerById(id);
            return customer is null ?
                NotFound(JsonConvert.SerializeObject("Ничего не найдено", Formatting.Indented)) :
                Ok(JsonConvert.SerializeObject(customer, Formatting.Indented));
        }

        // GET api/customers/VANYA/orders
        [HttpGet("{id}/orders")]
        public IActionResult GetOrdersByCustomerId(string id)
        {
            var orders = _customerService.GetOrdersByCustomerId(id);
            return orders is null ?
                NotFound(JsonConvert.SerializeObject("Ничего не найдено", Formatting.Indented)) :
                Ok(JsonConvert.SerializeObject(orders, Formatting.Indented));
        }

        // POST api/customers/
        [HttpPost] 
        public IActionResult CreateCustomer([FromBody]Customer customer)
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
