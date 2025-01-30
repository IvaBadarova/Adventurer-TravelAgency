using System.Diagnostics;

namespace TravelAgencyAdminApplication.Models
{
    public class ActivityInItinerary
    {
        public Guid Id { get; set; }
        public Guid ActivityId { get; set; }
        public Activity? Activity { get; set; }
        public Guid ItineraryId { get; set; }
        public Itinerary? Itinerary { get; set; }
    }
}
