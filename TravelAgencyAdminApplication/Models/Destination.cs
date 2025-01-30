namespace TravelAgencyAdminApplication.Models
{
    public class Destination
    {
        public Guid Id {  get; set; }
        public string? CityName { get; set; }
        public string? CountryName { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public ICollection<DestinationInPackage>? DestinationInPackages { get; set; }
    }
}
