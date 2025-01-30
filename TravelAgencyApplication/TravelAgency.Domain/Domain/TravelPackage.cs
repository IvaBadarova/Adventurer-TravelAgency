using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Domain.Domain
{
    public class TravelPackage : BaseEntity
    {
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
        public virtual ICollection<AccommodationInPackage>? AccommodationInPackages { get; set; }
        public virtual ICollection<DestinationInPackage>? DestinationInPackages { get; set; }
        public virtual ICollection<Itinerary>? Itineraries { get; set; }
        public virtual ICollection<Booking>? Bookings { get; set; }
        public virtual ICollection<PackageInOrder>? PackageInOrders { get; set; }

    }
}
