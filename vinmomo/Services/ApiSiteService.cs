using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using vinmomo.Models;

namespace vinmomo.Services
{
    public class ApiSiteService
    {
        private readonly HttpClient _http;

        public ApiSiteService()
        {
            _http = ApiClient.HttpClient;
        }

        public Task<List<Site>> GetSitesAsync()
            => _http.GetFromJsonAsync<List<Site>>("Site");

        public async Task AddSiteAsync(Site site)
        {
            var res = await _http.PostAsJsonAsync("Site", site);
            res.EnsureSuccessStatusCode();
        }

        public async Task UpdateSiteAsync(int id, Site site)
        {
            var res = await _http.PutAsJsonAsync($"Site/{id}", site);
            res.EnsureSuccessStatusCode();
        }

        public async Task DeleteSiteAsync(int id)
        {
            var res = await _http.DeleteAsync($"Site/{id}");
            res.EnsureSuccessStatusCode();
        }
    }
}
