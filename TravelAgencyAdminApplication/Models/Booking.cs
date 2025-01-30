namespace TravelAgencyAdminApplication.Models
{
    public class Booking
    {
        public Guid Id { get; set; }
        public DateOnly BookingDate { get; set; }
        public float Price { get; set; }
        public int NumberOfTravelers { get; set; }
        public Guid PackageId { get; set; }
        public TravelPackage? Package { get; set; }
        public string? CustomerId { get; set; }
        public Customer? Customer { get; set; }
    }
}
