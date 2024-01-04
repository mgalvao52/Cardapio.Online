using Cardapio.Application.DTOs;
using Newtonsoft.Json;

namespace Cardapio.Web.BlazorApp.Services
{
    public class MenuService
    {
        private readonly HttpClient _httpClient;

        public MenuService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient.CreateClient(nameof(MenuService));
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
