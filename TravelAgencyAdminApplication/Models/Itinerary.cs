namespace TravelAgencyAdminApplication.Models
{
    public class Itinerary
    {
        public Guid Id { get; set; }
        public int Day { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set; }
        public string? TransportationDetails { get; set; }
        public string? AccomodationDetails { get; set; }
        public Guid PackageId { get; set; }
        public TravelPackage? TravelPackage { get; set; }
        public ICollection<ActivityInItinerary>? ActivityInItineraries { get; set; }
    }
}
