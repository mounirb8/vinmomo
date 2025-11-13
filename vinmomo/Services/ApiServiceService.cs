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

        public async Task<List<Service>> GetServicesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Service>>("Service");
        }
    }
}
