namespace YupProject.Entities
{
    internal class Music
    {
        public string Name { get; set; }
        public string Artist { get; set; }
        public string NameArtist { get; set; }

        public Music(string name, string artist)
        {
            Name = name;
            Artist = artist;
            NameArtist = name + "(" + artist + ")";

        }

    }
}
