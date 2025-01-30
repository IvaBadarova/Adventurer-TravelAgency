namespace TravelAgency.Domain.Domain
{
    public class ActivityInItinerary : BaseEntity
    {
        public Guid ActivityId { get; set; }
        public Activity? Activity { get; set; }
        public Guid ItineraryId { get; set; }
        public Itinerary? Itinerary { get; set; }
    }
}
