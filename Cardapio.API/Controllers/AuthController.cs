using Cardapio.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Cardapio.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserService userService;

        public AuthController(UserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(TableDTO tableDTO)
        {
            try
            {
                if (tableDTO == null)
                {
                    return BadRequest("usuario ou senha invalida");
                }
                var result = await userService.Login(tableDTO);
                if (result)
                {
                    return Ok();
                }
                return BadRequest("usuario ou senha invalida");
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro interno");
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await userService.Logout();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro interno");
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var result = await userService.GetTablesAsync();
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro interno");
            }
        }
    }
}
