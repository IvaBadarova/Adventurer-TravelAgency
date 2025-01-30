using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using TravelAgency.Domain.Domain;
using TravelAgency.Repository;
using TravelAgency.Repository.Implementation;
using TravelAgency.Repository.Interface;
using TravelAgency.Service.Interface;

namespace TravelAgency.Service
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository<ShoppingCart> _cartRepository;
        private readonly IRepository<BookingInShoppingCart> _bookingInShoppingCartRepository;
        private readonly IRepository<TravelPackage> _packageRepository;
        private readonly IRepository<Booking> _bookingRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<PackageInOrder> _packageInOrderRepository;

        public ShoppingCartService(IRepository<ShoppingCart> cartRepository, IRepository<BookingInShoppingCart> bookingInShoppingCart, 
            IRepository<TravelPackage> packageRepository, IRepository<Booking> bookingRepository, IUserRepository userRepository,
            IRepository<Order> orderRepository, IRepository<PackageInOrder> packageInOrderRepository)
        {
            _cartRepository = cartRepository;
            _bookingInShoppingCartRepository = bookingInShoppingCart;
            _packageRepository = packageRepository;
            _bookingRepository = bookingRepository;
            _userRepository = userRepository;
            _orderRepository = orderRepository;
            _packageInOrderRepository = packageInOrderRepository;
        }

        public void AddToCart(Guid packageId, string customerId, int numberOfTravelers)
        {
            var cart = _cartRepository.GetAll().AsQueryable()
                .Include(c => c.ShoppingCartBookings).FirstOrDefault(c => c.CustomerId == customerId);
            if (cart == null)
            {
                cart = new ShoppingCart
                {
                    //customer
                    Id = Guid.NewGuid(),
                    CustomerId = customerId,
                    ShoppingCartBookings = new List<BookingInShoppingCart>()
                };
                _cartRepository.Insert(cart);
            }

            var travelPackage = _packageRepository.Get(packageId);
            if (travelPackage == null)
            {
                throw new Exception("Travel package not found.");
            }

            float totalPrice = travelPackage.Price * numberOfTravelers;

            var booking = new Booking
            {
                //customer
                Id = Guid.NewGuid(),
                PackageId = packageId,
                NumberOfTravelers = numberOfTravelers,
                BookingDate = DateOnly.FromDateTime(DateTime.Now),
                Price = totalPrice,
                CustomerId = customerId,
                Package = travelPackage
            };

            var cartBooking = new BookingInShoppingCart
            {
                Id = Guid.NewGuid(),
                Booking = booking,
                ShoppingCart = cart,
                ShoppingCartId = cart.Id,
                BookingId = booking.Id
            };
            travelPackage.Bookings.Add(booking);
            cart.ShoppingCartBookings.Add(cartBooking);
            _bookingInShoppingCartRepository.Insert(cartBooking);
        }


        public IEnumerable<Booking> GetCartItems(string customerId)
        {
            return _bookingInShoppingCartRepository.GetAll().AsQueryable()
                .Include(cb => cb.Booking)
                .ThenInclude(b => b.Package)
                .Where(cb => cb.ShoppingCart.CustomerId == customerId)
                .Select(cb => cb.Booking).ToList();
        }

        public void RemoveFromCart(Guid bookingId, string customerId)
        {
            var cartBooking = _bookingInShoppingCartRepository.GetAll().AsQueryable()
                .Include(cb => cb.ShoppingCart)
                .Include(cb => cb.Booking)
                .FirstOrDefault(cb => cb.Booking.Id == bookingId && cb.ShoppingCart.CustomerId == customerId);

            if (cartBooking != null)
            {
                _bookingInShoppingCartRepository.Delete(cartBooking);
                _bookingRepository.Delete(_bookingRepository.Get(bookingId));
            }
        }

        public void ConfirmCart(Guid bookingId,string customerId)
        {
            var cartBooking = _bookingInShoppingCartRepository.GetAll().AsQueryable()
                .Include(cb => cb.ShoppingCart)
                .Include(cb => cb.Booking)
                .FirstOrDefault(cb => cb.Booking.Id == bookingId && cb.ShoppingCart.CustomerId == customerId);

            if (cartBooking != null)
            {
                _bookingInShoppingCartRepository.Delete(cartBooking);
            }
        }

        public bool Order(string userId)
        {
            if (userId != null)
            {
                var loggedInUser = _userRepository.Get(userId);
                var userShoppingCart = _cartRepository.GetAll().FirstOrDefault(c => c.CustomerId == userId);

                Order order = new Order
                {
                    Id = Guid.NewGuid(),
                    CustomerId = userId,
                    Customer = _userRepository.Get(userId)
                };

                _orderRepository.Insert(order);

                List<PackageInOrder> packageInOrder = new List<PackageInOrder>();

                var lista = userShoppingCart.ShoppingCartBookings.Select(
                    x => new PackageInOrder
                    {
                        Id = Guid.NewGuid(),
                        PakcageId = x.Booking.PackageId,
                        Package = x.Booking.Package,
                        OrderId = order.Id,
                        Order = order,
                        Quantity = x.Booking.NumberOfTravelers
                    }
                    ).ToList();

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Your order is completed. The order conatins: ");

                var totalPrice = 0.0;

                for (int i = 1; i <= lista.Count(); i++)
                {
                    var currentItem = lista[i - 1];
                    totalPrice += currentItem.Quantity * currentItem.Package.Price;
                    sb.AppendLine(i.ToString() + ". " + currentItem.Package.Name + " with quantity of " + currentItem.Quantity + " and price of " + currentItem.Package.Price + "€");
                }

                sb.AppendLine("Total price for your order: " + totalPrice.ToString());

                packageInOrder.AddRange(lista);

                foreach (var product in packageInOrder)
                {
                    _packageInOrderRepository.Insert(product);
                }

                userShoppingCart.ShoppingCartBookings.Clear();

                _userRepository.Update(loggedInUser);
                return true;
            }
            return false;
        }
    }
}
