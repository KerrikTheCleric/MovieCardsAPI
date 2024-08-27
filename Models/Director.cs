namespace MovieCardsAPI.Models {
    public class Director {
        public long Id { get; set; }
        public string? Name { get; set; }
        public DateTime DateOfBirth { get; set; }

        public ICollection<Movie> Movies { get; set; }

        public ContactInformation ContactInformation { get; set; }
    }
}
