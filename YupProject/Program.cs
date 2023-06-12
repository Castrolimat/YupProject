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


            await Console.Out.WriteAsync("Select an option:\n" +
                 "1. Youtube to Spotify\n" +
                 "2. Spotify to Youtube\n" +
                 "?");

            int option = int.Parse(Console.ReadLine());

            switch (option)
            {
                case 1:
                    break;

                case 2:
                    List<Music> playlist = new List<Music>();
                    playlist = await spotifyApiService.GetPlaylist(spotifyApiService.GetPlaylistId(), spotifyAuthenticator.Token);

                    await Console.Out.WriteAsync("Nome da playlist(Youtube): ");
                    string nomePlaylist = Console.ReadLine();
                    string IdPlaylist = youtubeApiService.CreatePlaylist(nomePlaylist);
                    await youtubeApiService.InsertMusic(playlist, IdPlaylist);
                    await Console.Out.WriteLineAsync("Playlist criada com sucesso!!");
                    break;

                default: Console.WriteLine("Opção Inválida!"); break;
            }


        }
    }
}