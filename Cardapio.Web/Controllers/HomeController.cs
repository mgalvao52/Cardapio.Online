using Cardapio.Application.DTOs;
using Cardapio.Web.Models;
using Cardapio.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Cardapio.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MenuItemService menuItemService;
        private readonly CardapioService<AddOrderDTO,ReadOrderDTO> cardapioService;

        public HomeController(ILogger<HomeController> logger, MenuItemService menuItemService, CardapioService<AddOrderDTO,ReadOrderDTO> cardapioService)
        {
            _logger = logger;
            this.menuItemService = menuItemService;
            this.cardapioService = cardapioService;
        }
        [HttpGet("{numeroMesa}")]
        public async Task<IActionResult> Index(int numeroMesa)
        {
            var order = await cardapioService.GetAsync($"api/order/{numeroMesa}");
            var list = await menuItemService.GetAllAsync("api/menuItem");
            if (order != null)
            {
                if (TempData.ContainsKey("numeroPedido"))
                {
                    TempData["numeroPedido"] = order.Id;
                }
                else
                {
                    TempData.Add("numeroPedido",order.Id);

                }
            }
            if (TempData.ContainsKey("numeroMesa"))
            {
                TempData["numeroMesa"] = numeroMesa;
            }
            else
            {
                TempData.Add("numeroMesa",numeroMesa);

            }
            return View(list);
        }

        public async Task<IActionResult> Index()
        {
            var list = await menuItemService.GetAllAsync("api/menuItem");
            return View(list);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost("order")]
        public async Task<IActionResult> AddOrderAsync([FromBody]AddOrderDTO order)
        {
            if (!ModelState.IsValid)
            {
                var result = await cardapioService.PostAsync(order, "api/order");
                if (result)
                {
                    return Ok();

                }
            }
            return BadRequest();
        }
    }
}
