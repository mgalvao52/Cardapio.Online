using Newtonsoft.Json;

namespace Cardapio.Web.Services
{
    public class CardapioService<Add,Read> where Add : class where Read : class
    {
        private readonly HttpClient _httpClient;
        public CardapioService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(configuration.GetSection("UrlAPI").Value);
        }

        public async Task<bool> PostAsync(Add data,string route)
        {
            try
            {
                HttpResponseMessage resp = null;
                resp = await _httpClient.PostAsJsonAsync(route, data);
                var detalhe = await resp.Content.ReadAsStringAsync();
                return resp.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public async Task<bool> PutAsync(Add data, string route, bool multipart = false)
        {
            try
            {
                HttpResponseMessage resp = null;
                resp = await _httpClient.PutAsJsonAsync(route, data);
                return resp.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public async Task<bool> DeleteAsync(Add data, string route)
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
        public async Task<Read> GetAsync(string route)
        {
            var result = Activator.CreateInstance<Read>();
            try
            {
                HttpResponseMessage resp = null;
                resp = await _httpClient.GetAsync(route);
                if (resp.IsSuccessStatusCode)
                {
                    var json = await resp.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Read>(json);
                }
            }
            catch (Exception)
            {
            }
            return result;

        }
        public async Task<List<Read>> GetAllAsync(string route)
        {
            var result = new List<Read>();
            try
            {
                HttpResponseMessage resp = null;
                resp = await _httpClient.GetAsync(route);
                if (resp.IsSuccessStatusCode)
                {
                    var json = await resp.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<List<Read>>(json);
                }
            }
            catch (Exception)
            {
            }
            return result;
        }
       
    }
}
