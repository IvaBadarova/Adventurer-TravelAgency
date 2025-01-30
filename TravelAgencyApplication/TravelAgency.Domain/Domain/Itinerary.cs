using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Domain.Domain
{
    public class Itinerary : BaseEntity
    {
        public int Day { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set; }
        public string? TransportationDetails { get; set; }
        public string? AccomodationDetails { get; set; }
        public Guid PackageId { get; set; }
        public TravelPackage? TravelPackage { get; set; }
        public virtual ICollection<ActivityInItinerary>? ActivityInItineraries { get; set; }
    }
}
