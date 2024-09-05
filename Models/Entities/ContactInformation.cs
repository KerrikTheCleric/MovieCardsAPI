using System.ComponentModel.DataAnnotations;

namespace MovieCardsAPI.Models.Entities {
    public class ContactInformation {
        public long Id { get; set; }
        [Required]
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        //Foreign Key
        public long DirectorId { get; set; }

        //Navigationprop
        public Director Director { get; set; }
    }
}
