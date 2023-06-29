using YupProject.Entities;
using YupProject.Services;
using YupProject.Services.Authorization;


namespace YupProject
{
    internal class Program
    {

        static async Task Main(string[] args)
        {


            var spotifyAuthenticator = new SpotifyAuthenticator();
            spotifyAuthenticator.GetAcessToken();
            YoutubeApiService youtubeApiService = new YoutubeApiService();
            HttpClient httpClient = new HttpClient();
            var spotifyApiService = new SpotifyApiService(httpClient, spotifyAuthenticator.Token);
            List<Music> playlist = new List<Music>();

            playlist = await spotifyApiService.GetPlaylist(spotifyApiService.GetPlaylistId(), spotifyAuthenticator.Token);
            int op = 0;

            do
            {
                await Console.Out.WriteAsync("Nome da playlist(Youtube): ");
                string nomePlaylist = Console.ReadLine();
                string IdPlaylist = youtubeApiService.CreatePlaylist(nomePlaylist);
                await youtubeApiService.InsertMusic(playlist, IdPlaylist);
                await Console.Out.WriteLineAsync("Playlist criada com sucesso!!");
                await Console.Out.WriteLineAsync("Deseja adicionar outra playlist no youtube? (1) - Sim | 2 (Não)");
                op = int.Parse(Console.ReadLine());
            } while (op == 0);




        }
    }
}