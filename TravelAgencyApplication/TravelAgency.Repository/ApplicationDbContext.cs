using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TravelAgency.Domain.Domain;

namespace TravelAgency.Repository
{
    public class ApplicationDbContext : IdentityDbContext<Customer>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<Itinerary> Itineraries { get; set; }
        public virtual DbSet<TravelPackage> TravelPackages { get; set; }
        public virtual DbSet<Destination> Destinations { get; set; }
        public virtual DbSet<Accommodation> Accommodations { get; set; }
        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public virtual DbSet<BookingInShoppingCart> BookingInShoppingCarts { get; set; }
        public virtual DbSet<PackageInOrder> PackageInOrders { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
    }
}
