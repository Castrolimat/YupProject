using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using YupProject.Entities;
using YupProject.Services.Authorization;

namespace YupProject.Services
{
    internal class YoutubeApiService : YoutubeAuthenticator
    {
        public string CreatePlaylist(string title)
        {
            var playlist = new Playlist();
            playlist.Snippet = new PlaylistSnippet();
            playlist.Snippet.Title = title;
            playlist.Snippet.Description = "Playlist created using YupProject";
            playlist.Status = new PlaylistStatus();
            playlist.Status.PrivacyStatus = "public";

            var request = YoutubeService.Playlists.Insert(playlist, "snippet,status");
            var createdPlaylist = request.Execute();

            string ytplaylistId = createdPlaylist.Id;
            return ytplaylistId;
        }

        public async Task InsertMusic(List<Music> playlist, string playlistId)
        {
            foreach (Music music in playlist)
            {
                string keyword = music.NameArtist;
                SearchResource.ListRequest listRequest = YoutubeService.Search.List("snippet");
                listRequest.Q = keyword;
                listRequest.MaxResults = 1;
                SearchListResponse searchResponse = await listRequest.ExecuteAsync();
                foreach (SearchResult searchResult in searchResponse.Items)
                {
                    string videoId = searchResult.Id.VideoId;
                    var playlistItem = new PlaylistItem();
                    playlistItem.Snippet = new PlaylistItemSnippet()
                    {
                        PlaylistId = playlistId,
                        ResourceId = new ResourceId()
                        {
                            Kind = "youtube#video",
                            VideoId = videoId
                        }
                    };

                    try
                    {
                        // Chama o método PlaylistItems.Insert para adicionar o item da playlist
                        var insertRequest = YoutubeService.PlaylistItems.Insert(playlistItem, "snippet");
                        await insertRequest.ExecuteAsync();

                        Console.WriteLine(music.NameArtist + " adicionado à playlist com sucesso!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Falha ao adicionar o vídeo à playlist: " + ex.Message);
                    }
                }
            }
        }



    }
}
