using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Domain.Domain
{
    public class BookingInShoppingCart : BaseEntity
    {
        public Guid BookingId { get; set; }
        public Booking? Booking { get; set; }
        public Guid ShoppingCartId { get; set; }
        public ShoppingCart? ShoppingCart { get; set; }
    }
}
