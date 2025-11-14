using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using vinmomo.Models;

namespace vinmomo.Services
{
    public class ApiServiceService
    {
        private readonly HttpClient _httpClient;

        public ApiServiceService()
        {
            _httpClient = ApiClient.HttpClient;
        }

        // Récupérer tous les services
        public Task<List<Service>> GetServicesAsync()
        {
            return _httpClient.GetFromJsonAsync<List<Service>>("Service");
        }

        // Ajouter un service
        public async Task AddServiceAsync(Service service)
        {
            var response = await _httpClient.PostAsJsonAsync("Service", service);
            response.EnsureSuccessStatusCode();
        }

        // Mettre à jour un service
        public async Task UpdateServiceAsync(Service service)
        {
            var response = await _httpClient.PutAsJsonAsync($"Service/{service.Id}", service);
            response.EnsureSuccessStatusCode();
        }

        // Supprimer un service
        public async Task DeleteServiceAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"Service/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
