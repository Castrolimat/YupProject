using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;

namespace YupProject.Services.Authorization
{
    internal class YoutubeAuthenticator
    {
        private UserCredential _credential;
        private YouTubeService _youtubeService;


        public YoutubeAuthenticator()
        {
            // Carrega as credenciais do cliente
            _credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets
                {
                    ClientId = "756342637083-50dj5o3km3nadb278ftfqs3h51ed8k5g.apps.googleusercontent.com",
                    ClientSecret = "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"
                },
                new[] { YouTubeService.Scope.Youtube },
                "user",
                CancellationToken.None,
                new FileDataStore("token.json", true)
            ).Result;

            // Cria uma instância do serviço da API do YouTube
            _youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = _credential,
                ApplicationName = "YupProject"
            });
        }


        public string CreatePlaylist(string title, string description)
        {
            var playlist = new Playlist();
            playlist.Snippet = new PlaylistSnippet();
            playlist.Snippet.Title = title;
            playlist.Snippet.Description = description;
            playlist.Status = new PlaylistStatus();
            playlist.Status.PrivacyStatus = "public";

            var request = _youtubeService.Playlists.Insert(playlist, "snippet,status");
            var createdPlaylist = request.Execute();

            string ytplaylistId = createdPlaylist.Id;
            return ytplaylistId;
        }
    }
}
