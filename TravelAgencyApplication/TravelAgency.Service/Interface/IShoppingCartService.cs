using System;
using System.Collections.Generic;
using TravelAgency.Domain.Domain;

namespace TravelAgency.Service
{
    public interface IShoppingCartService
    {
        
        void AddToCart(Guid packageId, string customerId, int numberOfTravelers);

        
        IEnumerable<Booking> GetCartItems(string customerId);

        
        void RemoveFromCart(Guid bookingId, string customerId);

        
        void ConfirmCart(Guid bookingId,string customerId);
        bool Order(string userId);
    }
}
