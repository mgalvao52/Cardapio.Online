using Cardapio.Application.DTOs;
using Newtonsoft.Json;

namespace Cardapio.Web.BlazorApp.Services
{
    public class OrderMenuService
    {
        private readonly HttpClient _httpClient;

        public OrderMenuService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient.CreateClient(nameof(MenuService));
        }

        public async Task<ReadOrderDTO> GetOrderAsync(int tableNumber)
        {
            var result = new ReadOrderDTO();
            try
            {
                HttpResponseMessage resp = null;
                resp = await _httpClient.GetAsync($"api/order/{tableNumber}");
                if (resp.IsSuccessStatusCode)
                {
                    var json = await resp.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<ReadOrderDTO>(json);
                }
            }
            catch (Exception)
            {
            }
            return result;
        }

        public async Task<AddOrderDTO> AddOrderAsync(AddOrderDTO orderDTO)
        {
            AddOrderDTO result = null;
            try
            {
                HttpResponseMessage resp = null;
                resp = await _httpClient.PostAsJsonAsync($"api/order",orderDTO);
                if (resp.IsSuccessStatusCode)
                {
                    var json = await resp.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<AddOrderDTO>(json);
                }

            }
            catch (Exception)
            {
            }
            return result;
        }
        public async Task<bool> AddOrderItemAsync(AddOrderItemDTO orderDTO)
        {
            var result = false;
            try
            {
                HttpResponseMessage resp = null;
                resp = await _httpClient.PostAsJsonAsync($"api/orderitem", orderDTO);
                if (resp.IsSuccessStatusCode)
                {
                    var json = await resp.Content.ReadAsStringAsync();
                }

                return resp.IsSuccessStatusCode;
            }
            catch (Exception)
            {
            }
            return result;
        }
    }
}
