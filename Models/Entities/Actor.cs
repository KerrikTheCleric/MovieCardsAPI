using System.ComponentModel.DataAnnotations;

namespace MovieCardsAPI.Models.Entities {
    public class Actor {
        public long Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public DateOnly DateOfBirth { get; set; }

        //Navigationprop
        public ICollection<Movie> Movies { get; set; }
    }
}
