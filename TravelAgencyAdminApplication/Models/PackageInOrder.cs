namespace TravelAgencyAdminApplication.Models
{
    public class PackageInOrder
    {
        public Guid Id { get; set; }
        public Guid PakcageId { get; set; }
        public TravelPackage? Package { get; set; }
        public Guid OrderId { get; set; }
        public Order? Order { get; set; }
        public int Quantity { get; set; }
    }
}
