using System.ComponentModel.DataAnnotations;

namespace MovieCardsAPI.Models.Entities {
    public class Movie {
        public long Id { get; set; }
        [Required]
        public string Title { get; set; }

        [Range(0, 10)]
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