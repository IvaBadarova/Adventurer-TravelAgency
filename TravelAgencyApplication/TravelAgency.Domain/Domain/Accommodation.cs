using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Domain.Domain
{
    public class Accommodation : BaseEntity
    {
        public string? Name { get; set; }
        public string? Type { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
        public string? Image { get; set; }
        public virtual ICollection<AccommodationInPackage>? AccommodationInPackages { get; set; }
    }
}
