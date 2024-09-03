namespace MovieCardsAPI.Models.Entities {
    public class Director {
        public long Id { get; set; }
        public string? Name { get; set; }
        public DateTime DateOfBirth { get; set; }

        //Navigationprop
        public ICollection<Movie> Movies { get; set; }
        public ContactInformation ContactInformation { get; set; }
    }
}
