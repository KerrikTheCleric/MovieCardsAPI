namespace MovieCardsAPI.Models {
    public class Actor {
        public long Id { get; set; }
        public string? Name { get; set; }
        public DateTime DateOfBirth { get; set; }

        public ICollection<Movie> Movies { get; set; }

    }
}
