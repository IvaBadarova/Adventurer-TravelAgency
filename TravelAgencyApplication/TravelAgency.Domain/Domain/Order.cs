using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Domain.Domain
{
    public class Order : BaseEntity
    {
        public string? CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public IEnumerable<PackageInOrder>? PackageInOrders { get; set; }
    }
}
