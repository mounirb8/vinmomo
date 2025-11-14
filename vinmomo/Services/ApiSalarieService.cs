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

        // -------------------------------------------------
        //  MÉTHODE INTERNE : on nettoie l'objet avant envoi
        // -------------------------------------------------
        /// <summary>
        /// Ne garde que les champs scalaires + Id des FK.
        /// On supprime les objets de navigation pour éviter
        /// les erreurs EF Core (500 Internal Server Error).
        /// </summary>
        private Salarie Clean(Salarie s)
        {
            if (s == null) return null;

            return new Salarie
            {
                Id = s.Id,
                Nom = s.Nom,
                Prenom = s.Prenom,
                TelephoneFixe = s.TelephoneFixe,
                TelephonePortable = s.TelephonePortable,
                Email = s.Email,
                SiteId = s.SiteId,
                ServiceId = s.ServiceId,

                // IMPORTANT : ne jamais envoyer les objets liés
                Site = null,
                Service = null
            };
        }

        // ------------------------
        //  GET /api/Salarie
        // ------------------------
        public async Task<List<Salarie>> GetSalariesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Salarie>>("Salarie");
        }

        // ------------------------
        //  POST /api/Salarie
        // ------------------------
        public async Task AddSalarieAsync(Salarie salarie)
        {
            var clean = Clean(salarie);

            var response = await _httpClient.PostAsJsonAsync("Salarie", clean);
            response.EnsureSuccessStatusCode();
        }

        // ------------------------
        //  PUT /api/Salarie/{id}
        // ------------------------
        public async Task UpdateSalarieAsync(int id, Salarie salarie)
        {
            var clean = Clean(salarie);

            var response = await _httpClient.PutAsJsonAsync($"Salarie/{id}", clean);
            response.EnsureSuccessStatusCode();
        }

        // ------------------------
        //  DELETE /api/Salarie/{id}
        // ------------------------
        public async Task DeleteSalarieAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"Salarie/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
