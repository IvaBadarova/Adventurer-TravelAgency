namespace TravelAgencyAdminApplication.Models
{
    public class AccommodationInPackage
    {
        public Guid Id { get; set; }
        public Guid PackageId { get; set; }
        public TravelPackage? Package { get; set; }
        public Guid AccommodationId { get; set; }
        public Accommodation? Accommodation { get; set; }
    }
}
