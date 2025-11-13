using System;
using System.Net.Http;

namespace vinmomo.Services
{
    public class ApiClient
    {
        private static readonly HttpClient _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7174/api/") // remplace le port si différent
        };

        public static HttpClient HttpClient => _httpClient;
    }
}
