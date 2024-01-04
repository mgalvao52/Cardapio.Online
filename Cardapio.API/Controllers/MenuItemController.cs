using Cardapio.Application.DTOs;
using Cardapio.Application.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Cardapio.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuItemController : ControllerBase
    {
        private readonly IMenuItemService _service;
        private readonly ImageServices _imageServices;
        private readonly IHostEnvironment _hostEnvironment;

        public MenuItemController(IMenuItemService service, ImageServices imageServices,IHostEnvironment hostEnvironment)
        {
            this._service = service;
            this._imageServices = imageServices;
            this._hostEnvironment = hostEnvironment;
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AddAsync([FromForm]AddMenuItemDTO menuItemDTO)
        {
            try
            {
                if (menuItemDTO.ImageFile == null || menuItemDTO.ImageFile.Length <= 0)
                {
                    return BadRequest("Imagem é obrigatoria");
                }
                await GetImageData(menuItemDTO);
                if (_service.Validate(menuItemDTO))
                {
                    //menuItemDTO.Image = await _imageServices.AddImageAsync(menuItemDTO.Image);
                    await _service.CreateAsync(menuItemDTO);
                    return Created("", menuItemDTO);
                }
                return BadRequest(_service.responseMessage.Erros);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        private async Task GetImageData(AddMenuItemDTO menuItemDTO)
        {
            string directory = GetDirectory();
            var fileName = $"{directory}{menuItemDTO.ImageFile.FileName}";
            using (var stream = new FileStream(fileName, FileMode.Create))
            {
                await menuItemDTO.ImageFile.CopyToAsync(stream);
            }
            menuItemDTO.Image = $"{Request.Scheme}://{Request.Host.Value}/images/{menuItemDTO.ImageFile.FileName}";
        }

        private string GetDirectory()
        {
            var directory = $"{_hostEnvironment.ContentRootPath}images\\";
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            return directory;
        }

        [HttpGet("/images/{fileName}")]
        public async Task<IActionResult> GetImage(string fileName)
        {
            fileName = $"{GetDirectory()}{fileName}";
            FileStream stream = System.IO.File.Open(fileName,FileMode.Open);
            return File(stream, "image/jpeg");
        }

        [HttpPut]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateAsync([FromForm]AddMenuItemDTO menuItemDTO)
        {
            try
            {
                if (menuItemDTO.ImageFile == null || menuItemDTO.ImageFile.Length <= 0)
                {
                    return BadRequest("Imagem é obrigatoria");
                }
                if (_service.Validate(menuItemDTO))
                {
                    //menuItemDTO.Image = await _imageServices.AddImageAsync(menuItemDTO.Image);
                    await GetImageData(menuItemDTO);
                    await _service.UpdateAsync(menuItemDTO);
                    return NoContent();
                }
                return BadRequest(_service.responseMessage.Erros);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeteleAsync(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var result = await _service.GetAsync(s => s.Id == id);
                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var result = await _service.GetAllAsync();
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
