

using MovieCardsAPI.Models.Entities;

namespace MovieCardsAPI.Models.Dtos {
    public record MovieDto(long Id, string Title, short Rating, DateTime ReleaseDate, string Description, string DirectorName);

    public class MovieDetailsDTO {
        public long Id { get; set; }
        public string Title { get; set; }
        public short Rating { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; }
        public Director Director { get; set; }
        public IEnumerable<Actor> Actors { get; set; }
        public ICollection<Genre> Genres { get; set; }
    }
    public record MovieForCreationDTO(string Title, short Rating, DateTime ReleaseDate, string Description, long DirectorId);

    public record MovieForUpdateDTO(long Id, string Title, short Rating, DateTime ReleaseDate, string Description, long DirectorId);

}
