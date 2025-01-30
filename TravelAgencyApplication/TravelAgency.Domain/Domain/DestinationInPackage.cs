namespace TravelAgency.Domain.Domain
{
    public class DestinationInPackage : BaseEntity
    {
        public Guid PackageId { get; set; }
        public TravelPackage? TravelPackage { get; set; }
        public Guid DestinationId { get; set; }
        public Destination? Destination { get; set; }
    }
}
