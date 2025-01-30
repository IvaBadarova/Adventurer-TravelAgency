namespace TravelAgencyAdminApplication.Models
{
    public class Activity
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public float Price { get; set; }
        public string? Image { get; set; }
        public ICollection<ActivityInItinerary>? ActivityInItineraries { get; set; }
    }
}
