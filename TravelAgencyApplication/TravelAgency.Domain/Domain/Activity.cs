namespace TravelAgency.Domain.Domain
{
    public class Activity : BaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public float Price { get; set; }
        public string? Image { get; set; }
        public virtual ICollection<ActivityInItinerary>? ActivityInItineraries { get; set; }
    }
}
