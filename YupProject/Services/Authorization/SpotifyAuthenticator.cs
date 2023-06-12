using System.Text.Json;

namespace YupProject.Services.Authorization
{
    internal class SpotifyAuthenticator
    {
        private string ClientId;
        private string ClientSecret;
        public string Token { get; private set; }

        public SpotifyAuthenticator()
        {
            ClientId = "631f8cbcea9648e799847dca22476346";
            ClientSecret = "be5ec21016984c2ba692516c653b151a";
        }

        public async void GetAcessToken()
        {
            // Construir o corpo da requisição
            var requestBody = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "client_credentials"),
                new KeyValuePair<string, string>("client_id", ClientId),
                new KeyValuePair<string, string>("client_secret", ClientSecret)
            });

            // Enviar a requisição POST para obter o token de acesso
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsync("https://accounts.spotify.com/api/token", requestBody);
                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var json = JsonDocument.Parse(responseBody).RootElement;

                    Token = json.GetProperty("access_token").GetString();
                }
                else
                {
                    // Lidar com o erro de resposta, se necessário
                    Token = null;
                }

            }

        }
    }
}
