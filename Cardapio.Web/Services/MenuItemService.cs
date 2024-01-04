using Cardapio.Application.DTOs;
using Newtonsoft.Json;
using System.Net.Mime;
using System.Text;

namespace Cardapio.Web.Services
{
    public class MenuItemService
    {
        private readonly HttpClient _httpClient;
        public MenuItemService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(configuration.GetSection("UrlAPI").Value);
        }

        public async Task<bool> PostAsync(AddMenuItemDTO data,string route)
        {
            try
            {
                HttpResponseMessage resp = null;
                var stream = new MemoryStream();

                await data.ImageFile.CopyToAsync(stream);
                stream.Seek(0, SeekOrigin.Begin);
                var multipartContent = new MultipartFormDataContent();
                multipartContent.Add(new StreamContent(stream), data.ImageFile.Name, data.ImageFile.FileName);
                multipartContent.Add(new StringContent(data.Name, Encoding.UTF8, MediaTypeNames.Text.Plain),nameof(data.Name));
                multipartContent.Add(new StringContent(data.Description, Encoding.UTF8, MediaTypeNames.Text.Plain), nameof(data.Description));
                multipartContent.Add(new StringContent(data.Price.ToString(), Encoding.UTF8, MediaTypeNames.Text.Plain), nameof(data.Price));
                multipartContent.Add(new StringContent(data.Time.ToString(), Encoding.UTF8, MediaTypeNames.Text.Plain), nameof(data.Time));
                resp = await _httpClient.PostAsync(route, multipartContent);

                var detalhe = await resp.Content.ReadAsStringAsync();
                return resp.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public async Task<bool> PutAsync(AddMenuItemDTO data, string route)
        {
            try
            {
                HttpResponseMessage resp = null;
                using (var stream = new MemoryStream())
                {
                    await data.ImageFile.CopyToAsync(stream);
                    stream.Seek(0, SeekOrigin.Begin);
                    var multipartContent = new MultipartFormDataContent();
                    multipartContent.Add(new StreamContent(stream), data.ImageFile.Name, data.ImageFile.FileName);
                    multipartContent.Add(new StringContent(data.Name, Encoding.UTF8, MediaTypeNames.Text.Plain), nameof(data.Name));
                    multipartContent.Add(new StringContent(data.Description, Encoding.UTF8, MediaTypeNames.Text.Plain), nameof(data.Description));
                    multipartContent.Add(new StringContent(data.Price.ToString(), Encoding.UTF8, MediaTypeNames.Text.Plain), nameof(data.Price));
                    multipartContent.Add(new StringContent(data.Time.ToString(), Encoding.UTF8, MediaTypeNames.Text.Plain), nameof(data.Time));
                    multipartContent.Add(new StringContent(data.Id.ToString(), Encoding.UTF8, MediaTypeNames.Text.Plain), nameof(data.Id));
                    resp = await _httpClient.PutAsync(route, multipartContent);
                }
                var detalhe = await resp.Content.ReadAsStringAsync();
                return resp.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public async Task<bool> DeleteAsync(string route)
        {
            try
            {
                HttpResponseMessage resp = null;
                resp = await _httpClient.DeleteAsync(route);
                return resp.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public async Task<ReadMenuItemDTO> GetAsync(string route)
        {
            var result = Activator.CreateInstance<ReadMenuItemDTO>();
            try
            {
                HttpResponseMessage resp = null;
                resp = await _httpClient.GetAsync(route);
                if (resp.IsSuccessStatusCode)
                {
                    var json = await resp.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ReadMenuItemDTO>(json);
                }
            }
            catch (Exception)
            {
            }
            return result;

        }
        public async Task<List<ReadMenuItemDTO>> GetAllAsync(string route)
        {
            var result = new List<ReadMenuItemDTO>();
            try
            {
                HttpResponseMessage resp = null;
                resp = await _httpClient.GetAsync(route);
                if (resp.IsSuccessStatusCode)
                {
                    var json = await resp.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<List<ReadMenuItemDTO>>(json);
                }
            }
            catch (Exception)
            {
            }
            return result;
        }
       
    }
}
