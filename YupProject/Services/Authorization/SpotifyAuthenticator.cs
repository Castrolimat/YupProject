using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace YupProject.Services.Authorization
{
    internal class SpotifyAuthenticator
    {
        private readonly string _clientId;
        private readonly string _clientSecret;

        public SpotifyAuthenticator(string clientId, string clientSecret)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
        }

        public async Task<string> GetAcessToken()
        {
            // Construir o corpo da requisição
            var requestBody = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "client_credentials"),
                new KeyValuePair<string, string>("client_id", _clientId),
                new KeyValuePair<string, string>("client_secret", _clientSecret)
            });

            // Enviar a requisição POST para obter o token de acesso
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsync("https://accounts.spotify.com/api/token", requestBody);
                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var json = JsonDocument.Parse(responseBody).RootElement;

                    return json.GetProperty("access_token").GetString();
                }
                else
                {
                    // Lidar com o erro de resposta, se necessário
                    return null;
                }

            }

        }
    }
}
