namespace TravelAgencyAdminApplication.Models
{
    public class TravelPackage
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public int DurationInDays { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public float Price { get; set; }
        public string? Type { get; set; }
        public int MaxSpots { get; set; }
        public string? TransportationDetails { get; set; }
        public ICollection<AccommodationInPackage>? AccommodationInPackages { get; set; }
        public ICollection<DestinationInPackage>? DestinationInPackages { get; set; }
        public ICollection<Itinerary>? Itineraries { get; set; }
        public ICollection<Booking>? Bookings { get; set; }
        public ICollection<PackageInOrder>? PackageInOrders { get; set; }
    }
}
