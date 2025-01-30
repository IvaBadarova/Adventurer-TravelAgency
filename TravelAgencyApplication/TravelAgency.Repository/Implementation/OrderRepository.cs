using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Domain;
using TravelAgency.Repository.Interface;

namespace TravelAgency.Repository.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<Order> entities;

        public OrderRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<Order>();
        }
        public List<Order> GetAllOrders()
        {
            return entities
                .Include(z => z.PackageInOrders)
                .Include(z => z.Customer)
                .Include("PackageInOrders.Package")
                .Include("PackageInOrders.Package.AccommodationInPackages.Accommodation")
                .Include("PackageInOrders.Package.DestinationInPackages.Destination")
                .Include("PackageInOrders.Package.Itineraries.ActivityInItineraries.Activity")
                .ToList();
        }

        public Order GetDetailsForOrder(BaseEntity id)
        {
            return entities
                .Include(z => z.PackageInOrders)
                .Include(z => z.Customer)
                .Include("PackageInOrders.Package")
                .SingleOrDefaultAsync(z => z.Id == id.Id).Result;
        }
    }
}
