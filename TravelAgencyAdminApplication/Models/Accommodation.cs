namespace TravelAgencyAdminApplication.Models
{
    public class Accommodation
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
        public string? Image { get; set; }
        public ICollection<AccommodationInPackage>? AccommodationInPackages { get; set; }
    }
}
