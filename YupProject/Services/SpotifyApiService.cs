using System.Net.Http.Headers;

namespace YupProject.Services
{
    internal static class SpotifyApiService
    {
        private static readonly HttpClient _httpClient;
        private static readonly string _accessToken;



        public static string GetPlaylistId(string playlistUrl)
        {
            Console.Write("Link da playlist: ");
            playlistUrl = Console.ReadLine();
            int startIndex = playlistUrl.LastIndexOf('/') + 1;
            int endIndex = playlistUrl.LastIndexOf("?");
            string playlistId = playlistUrl.Substring(startIndex, endIndex - startIndex);
            Console.WriteLine(playlistId);
            return playlistId;
        }

        public async static Task<string> GetPlaylist(string playlistId,string _accessToken)
        {
            string url = $"https://api.spotify.com/v1/playlists/{playlistId}";

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);

            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
            else
            {
                throw new Exception("A requisição não foi bem-sucedida. Código de status: " + response.StatusCode);
            }
        }
    }
}
