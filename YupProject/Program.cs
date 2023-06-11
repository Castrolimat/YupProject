using YupProject.Services;
using YupProject.Services.Authorization;

namespace YupProject
{
    internal class Program
    {

        static async Task Main(string[] args)
        {
            string clientId = "631f8cbcea9648e799847dca22476346";
            string clientSecret = "be5ec21016984c2ba692516c653b151a";

            var SpotifyAuthenticator = new SpotifyAuthenticator(clientId, clientSecret);
            HttpClient httpClient = new HttpClient();

            try
            {
                string accessToken = await SpotifyAuthenticator.GetAcessToken();
                string playlistId = SpotifyApiService.GetPlaylistId(accessToken);
                var SpotifyApiRequests = new SpotifyApiService(httpClient, accessToken);
                Task<string> playlist = SpotifyApiRequests.GetPlaylist(playlistId, accessToken);
                await Console.Out.WriteLineAsync(playlist);

            }
            catch (Exception error)
            {
                await Console.Out.WriteLineAsync(error.Message);
                throw;
            }



        }
    }
}