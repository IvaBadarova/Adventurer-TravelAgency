using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Domain.Domain
{
    public class ShoppingCart : BaseEntity
    {
        public string? CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public virtual ICollection<BookingInShoppingCart>? ShoppingCartBookings { get; set; }
    }
}
