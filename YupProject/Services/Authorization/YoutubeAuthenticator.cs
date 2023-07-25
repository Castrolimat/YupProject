using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Newtonsoft.Json.Linq;

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
                    ClientId = "",
                    ClientSecret = ""
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
