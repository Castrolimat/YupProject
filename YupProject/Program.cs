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

            var authenticator = new SpotifyAuthenticator(clientId, clientSecret);

            try
            {
                string accessToken = await authenticator.GetAcessToken();
                string playlistId = SpotifyApiService.GetPlaylistId(accessToken);
                Task<string> playlist = SpotifyApiService.GetPlaylist(playlistId, accessToken);

                await Console.Out.WriteLineAsync(await playlist);


            }
            catch (Exception error)
            {
                await Console.Out.WriteLineAsync(error.Message);
                throw;
            }



        }
    }
}