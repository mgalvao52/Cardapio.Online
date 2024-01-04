using Cardapio.Application.DTOs;
using Cardapio.Application.RabbitMQ.Interface;
using Cardapio.Application.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Cardapio.API.Controllers
{
    [Route("api/[controller]")]
    public class OrderController:ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IProducer _producer;

        public OrderController(IOrderService orderService,IProducer producer)
        {
            _orderService = orderService;
            this._producer = producer;
        }
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody]AddOrderDTO orderDTO)
        {
            try
            {
                var result = await _orderService.CreateAsync(orderDTO);
                if (_orderService.responseMessage.IsValid)
                {
                    foreach (var item in orderDTO.OrderItems)
                    {
                        _producer.SendMessage(item);
                    }
                    orderDTO.Id = result;
                    return Created("", orderDTO);
                }
                return BadRequest(_orderService.responseMessage.Erros);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
               await _orderService.DeleteAsync(id);                
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

        }
        [HttpGet("{numeroMesa}")]
        public async Task<IActionResult> GetByNumerMesaAsync(int numeroMesa)
        {
            try
            {
                var result = await _orderService.GetWithItemsAsync(s=>s.Number == numeroMesa && s.Status == DB.Enums.OrderStatus.Openned);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

        }
    }
}
