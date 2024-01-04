using Cardapio.Application.DTOs;
using Cardapio.Application.Services.Interface;
using Cardapio.DB.Migrations;
using Microsoft.AspNetCore.Mvc;

namespace Cardapio.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController:ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] AddPaymentDTO addPayment)
        {
            try
            {
                await _paymentService.CreateAsync(addPayment);
                if (_paymentService.responseMessage.IsValid)
                {
                    return Created("",addPayment);
                }
                return BadRequest(_paymentService.responseMessage.Erros);
            }
            catch (Exception)
            {
                return StatusCode(500, "erro interno");
            }
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetByOrderAsync(int orderId)
        {
            try
            {
                var result = await _paymentService.GetAsync(s => s.OrderId == orderId);
                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(500, "erro interno");
            }
        }
    }
}
