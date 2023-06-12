using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;

namespace YupProject.Services.Authorization
{
    internal class YoutubeAuthenticator
    {
        protected UserCredential UserCredential;
        protected YouTubeService YoutubeService { get; set; }


        public YoutubeAuthenticator()
        {
            // Carrega as credenciais do cliente
            UserCredential = GoogleWebAuthorizationBroker.AuthorizeAsync(
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
            YoutubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = UserCredential,
                ApplicationName = "YupProject"
            });
        }



    }
}
