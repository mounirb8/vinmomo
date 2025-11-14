using System;
using System.Net.Http;
using System.Net;

namespace vinmomo.Services
{
    public class ApiClient
    {
        private static readonly HttpClient _httpClient;

        static ApiClient()
        {
            var handler = new HttpClientHandler
            {
                // 🔥 Autoriser le certificat local HTTPS
                ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };

            _httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri("https://localhost:7174/api/") // ton API
            };
        }

        public static HttpClient HttpClient => _httpClient;
    }
}
