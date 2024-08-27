namespace MovieCardsAPI.Models {
    public class ContactInformation {
        public long Id { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        public Director Director { get; set; }
    }
}
