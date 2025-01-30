using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Domain.Domain
{
    public class Destination : BaseEntity
    {
        public string? CityName { get; set; }
        public string? CountryName { get; set; }
        public string? Description { get; set; }
        public string? Image {  get; set; }
        public virtual ICollection<DestinationInPackage>? DestinationInPackages { get; set; }
    }
}
