using Microsoft.AspNetCore.Mvc;
using NorthWindAPI.Models;
using NorthWindAPI.Paginations;
using NorthWindAPI.Services.Interfaces;

namespace NorthWindAPI.Controllers
{
    [Route("api/orders/")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private string _notFoundMessage = "Ничего не найдено";
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET api/orders
        [HttpGet]
        public IActionResult GetAllProduct([FromQuery] OrderParameters filter)
        {
            filter.Collection = _orderService.GetAllOrders();
            string response = filter.ResultProcessing();
            return filter.IsSuccess ? Ok(response) : NotFound(_notFoundMessage);
        }

        // GET api/orders/5
        [HttpGet("{id}")]
        public IActionResult GetProductById(int id, [FromQuery] string? format)
        {
            try
            {
                var order = _orderService.GetOrderById(id);
                return format == "xml" ? Ok(Converter.ToXml(order)) : Ok(Converter.ToJson(order));
            }

            catch (Exception ex)
            {
                return format == "xml" ? Ok(Converter.ToXml(_notFoundMessage)) : Ok(Converter.ToJson(_notFoundMessage));
            }
        }

        // POST api/orders/
        [HttpPost]
        public IActionResult CreateOrder([FromBody] Order order)
        {
            try
            {
                if (order is null)
                    return BadRequest("Передано пустое значение");

                _orderService.CreateOrder(order);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/orders/
        [HttpPut]
        public IActionResult UpdateOrder([FromBody] Order order)
        {
            try
            {
                if (order is null)
                    return BadRequest("Передано пустое значение");

                _orderService.UpdateOrder(order);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/orders/5
        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            try
            {
                _orderService.DeleteOrder(id);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
