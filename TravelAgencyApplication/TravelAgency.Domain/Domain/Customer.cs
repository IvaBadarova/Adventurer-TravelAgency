using Microsoft.AspNetCore.Identity;

namespace TravelAgency.Domain.Domain
{
    public class Customer : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public virtual ICollection<Booking>? Bookings { get; set; }
    }
}
