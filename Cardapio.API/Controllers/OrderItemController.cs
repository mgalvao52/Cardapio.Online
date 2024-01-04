using Cardapio.Application.DTOs;
using Cardapio.Application.RabbitMQ.Interface;
using Cardapio.Application.Services.Interface;
using Cardapio.DB.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Cardapio.API.Controllers
{
    [Route("api/[controller]")]
    public class OrderItemController:ControllerBase
    {
        private readonly IOrderItemService _orderItemService;
        private readonly IProducer _producer;

        public OrderItemController(IOrderItemService orderItemService,IProducer producer)
        {
            _orderItemService = orderItemService;
            this._producer = producer;
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] AddOrderItemDTO orderDTO)
        {
            try
            {
               var result = await _orderItemService.CreateAsync(orderDTO);
                if (_orderItemService.responseMessage.IsValid)
                {
                    var item = await _orderItemService.GetByIdAsync(result);
                    _producer.SendMessage(item);
                    return Created("", orderDTO);
                }
                return BadRequest(_orderItemService.responseMessage.Erros);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

        }

        [HttpPut("{itemId}/{status}")]
        public async Task<IActionResult> UpdateStatusAsync(int itemId,OrderItemStatus orderItemStatus)
        {
            try
            {
                await _orderItemService.UpdateStatusAsync(itemId,orderItemStatus);
                if (_orderItemService.responseMessage.IsValid)
                {
                    var menuItem = await _orderItemService.GetByIdAsync(itemId);
                    //_producer.SendMessage(menuItem);
                    return NoContent();
                }
                return BadRequest(_orderItemService.responseMessage.Erros);
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
                await _orderItemService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

        }
    }
}
