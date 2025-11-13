using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using vinmomo.Models;

namespace vinmomo.Services
{
    public class ApiSalarieService
    {
        private readonly HttpClient _httpClient;

        public ApiSalarieService()
        {
            _httpClient = ApiClient.HttpClient;
        }

        public async Task<List<Salarie>> GetSalariesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Salarie>>("Salarie");
        }

        public async Task AddSalarieAsync(Salarie salarie)
        {
            var response = await _httpClient.PostAsJsonAsync("Salarie", salarie);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateSalarieAsync(int id, Salarie salarie)
        {
            var response = await _httpClient.PutAsJsonAsync($"Salarie/{id}", salarie);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteSalarieAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"Salarie/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
