using Cardapio.Application.DTOs;
using Cardapio.Web.Models;
using Cardapio.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Cardapio.Web.Controllers
{
    public class MenuController:Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MenuItemService _cardapioService;
        public MenuController(ILogger<HomeController> logger, MenuItemService cardapioService)
        {
            _logger = logger;
            _cardapioService = cardapioService;
        }

        public async Task<IActionResult> Lista()
        {
            var list = await _cardapioService.GetAllAsync("api/menuItem");
            return View(list);
        }
        public async Task<IActionResult> Novo()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Novo([FromForm]AddMenuItemDTO menuItemDTO)
        {
            if (ModelState.IsValid)
            {
                var resp = await _cardapioService.PostAsync(menuItemDTO, "api/menuItem");
                if (resp)
                {
                    return RedirectToAction("Index");
                }
                return View(menuItemDTO);
            }
            return View(menuItemDTO);
        }

        public async Task<IActionResult> Alterar(int id)
        {
            var result = await _cardapioService.GetAsync($"api/menuItem/{id}");
            return View(result);
        }

        public async Task<IActionResult> Detalhes(int id)
        {
            var result = await _cardapioService.GetAsync($"api/menuItem/{id}");
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Alterar([FromForm]AddMenuItemDTO menuItemDTO)
        {
            if (ModelState.IsValid)
            {
                var resp = await _cardapioService.PutAsync(menuItemDTO, "api/menuItem");
                if (resp)
                {
                    return RedirectToAction("Index");
                }
                return View(menuItemDTO);
            }
            return View(menuItemDTO);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _cardapioService.DeleteAsync($"api/menuItem/{id}");
            return View(result);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

   
}
