namespace TravelAgencyAdminApplication.Models
{
    public class DestinationInPackage
    {
        public Guid Id { get; set; }
        public Guid PackageId { get; set; }
        public TravelPackage? TravelPackage { get; set; }
        public Guid DestinationId { get; set; }
        public Destination? Destination { get; set; }
    }
}
