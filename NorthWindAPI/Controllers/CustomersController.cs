using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NorthWindAPI.Models;
using NorthWindAPI.Services.Interfaces;

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
        public IActionResult Get()
        {
            var customers = _customerService.GetAllCustomers();
            return customers is null || customers.Count() == 0 ?
                 NotFound(JsonConvert.SerializeObject("Ничего не найдено",Formatting.Indented)):
                 Ok(JsonConvert.SerializeObject(customers, Formatting.Indented));
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
    }
}
