using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Domain.Domain
{
    public class Booking : BaseEntity
    {
        public DateOnly BookingDate { get; set; }
        public float Price { get; set; }
        public int NumberOfTravelers { get; set; }
        public Guid PackageId { get; set; }
        public TravelPackage? Package { get; set; }
        public string? CustomerId { get; set; }
        public Customer? Customer { get; set; }
    }
}
