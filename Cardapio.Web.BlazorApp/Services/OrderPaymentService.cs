using Cardapio.Application.DTOs;

namespace Cardapio.Web.BlazorApp.Services
{
    public class OrderPaymentService
    {
        private readonly HttpClient _httpClient;

        public OrderPaymentService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient.CreateClient(nameof(MenuService));
        }

        public async Task<bool> AddPaymentAsync(AddPaymentDTO addPayment)
        {
            var result = false;
            try
            {
                HttpResponseMessage resp = null;
                resp = await _httpClient.PostAsJsonAsync($"api/payment", addPayment);
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
