﻿namespace TravelAgencyAdminApplication.Models
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public ICollection<Booking>? Bookings { get; set; }
    }
}
