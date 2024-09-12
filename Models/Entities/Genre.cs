using System.ComponentModel.DataAnnotations;


namespace MovieCardsAPI.Models.Entities {
    public class Genre {
        public long Id { get; set; }
        [Required]
        public string? Name { get; set; }

        //Navigationprop
        public ICollection<Movie> Movies { get; set; }

        /*public override string ToString() {
            return Name;
        }*/
    }
}
