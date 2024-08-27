namespace MovieCardsAPI.Models {
    public class Movie {
        public long Id { get; set; }
        public string? Title { get; set; }
        public short Rating { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string? Description { get; set; }

        public Director Director { get; set; }
        public ICollection<Actor> Actors { get; set; }
        public ICollection<Genre> Genres { get; set; }
    }
}
