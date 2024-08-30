namespace MovieCardsAPI.Models.Entities
{
    public class Genre
    {
        public long Id { get; set; }
        public string? Name { get; set; }

        public ICollection<Movie> Movies { get; set; }

    }
}
