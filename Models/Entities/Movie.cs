namespace MovieCardsAPI.Models.Entities
{
    public class Movie
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public short Rating { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; }

        //Foreign Key
        public long DirectorId { get; set; }

        //Navigationprops
        public Director Director { get; set; }
        public ICollection<Actor> Actors { get; set; }
        public ICollection<Genre> Genres { get; set; }
    }
}