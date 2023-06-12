using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using YupProject.Entities;

namespace YupProject.Services
{
    internal class SpotifyApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _accessToken;

        public SpotifyApiService(HttpClient httpClient, string accessToken)
        {
            _httpClient = httpClient;
            _accessToken = accessToken;
        }


        public string GetPlaylistId()
        {
            Console.Write("Link da playlist: ");
            string playlistUrl = Console.ReadLine();
            int startIndex = playlistUrl.LastIndexOf('/') + 1;
            int endIndex = playlistUrl.LastIndexOf("?");
            return playlistUrl.Substring(startIndex, endIndex - startIndex);

        }

        public async Task<List<Music>> GetPlaylist(string playlistId, string _accessToken)
        {
            string url = $"https://api.spotify.com/v1/playlists/{playlistId}";

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);

            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string playlistJson = await response.Content.ReadAsStringAsync();

                JObject playlistObject = JObject.Parse(playlistJson);

                List<Music> playlist = new List<Music>();

                foreach (var track in playlistObject["tracks"]["items"])
                {


                    string musicName = track["track"]["name"].ToString();
                    string musicArtist = (string)track["track"]["artists"][0]["name"];

                    Music music = new Music(musicName, musicArtist);
                    playlist.Add(music);

                }

                return playlist;

            }
            else
            {
                throw new Exception("A requisição não foi bem-sucedida. Código de status: " + response.StatusCode);
            }

        }
    }
}
