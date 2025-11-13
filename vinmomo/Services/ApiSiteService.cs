using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using vinmomo.Models;

namespace vinmomo.Services
{
    public class ApiSiteService
    {
        private readonly HttpClient _httpClient;

        public ApiSiteService()
        {
            _httpClient = ApiClient.HttpClient;
        }

        public async Task<List<Site>> GetSitesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Site>>("Site");
        }
    }
}
