using System.Text.Json;

namespace YupProject.Services.Authorization
{
    internal class SpotifyAuthenticator
    {
        private readonly string _clientId;
        private readonly string _clientSecret;

        public SpotifyAuthenticator()
        {
            _clientId = "631f8cbcea9648e799847dca22476346";
            _clientSecret = "be5ec21016984c2ba692516c653b151a";
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
