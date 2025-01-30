namespace TravelAgency.Domain.Domain
{
    public class AccommodationInPackage : BaseEntity
    {
        public Guid PackageId { get; set; }
        public TravelPackage? Package { get; set; }
        public Guid AccommodationId { get; set; }
        public Accommodation? Accommodation { get; set; }
    }
}
