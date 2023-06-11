using YupProject.Entities;
using YupProject.Services;
using YupProject.Services.Authorization;


namespace YupProject
{
    internal class Program
    {

        static async Task Main(string[] args)
        {


            var SpotifyAuthenticator = new SpotifyAuthenticator();
            HttpClient httpClient = new HttpClient();

            try
            {
                string accessToken = await SpotifyAuthenticator.GetAcessToken();
                string playlistId = SpotifyApiService.GetPlaylistId(accessToken);
                var SpotifyApiRequests = new SpotifyApiService(httpClient, accessToken);
                List<Music> playlist = await SpotifyApiRequests.GetPlaylist(playlistId, accessToken);



            }
            catch (Exception error)
            {
                await Console.Out.WriteLineAsync(error.Message);
                throw;
            }

            var authenticator = new YoutubeAuthenticator();
            string ytplaylistId = authenticator.CreatePlaylist("Teste", "Descrição da minha playlist");
            Console.WriteLine("Playlist criada com sucesso. ID da playlist: " + ytplaylistId);


        }
    }
}