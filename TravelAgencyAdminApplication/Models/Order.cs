namespace TravelAgencyAdminApplication.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public string? CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public IEnumerable<PackageInOrder>? PackageInOrders { get; set; }
    }
}
