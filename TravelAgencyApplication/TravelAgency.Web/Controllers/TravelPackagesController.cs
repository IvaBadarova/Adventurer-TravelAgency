using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TravelAgency.Domain.Domain;
using TravelAgency.Repository;
using TravelAgency.Service;
using TravelAgency.Service.Implementation;
using TravelAgency.Service.Interface;

namespace TravelAgency.Web.Controllers
{
    public class TravelPackagesController : Controller
    {
        private readonly ITravelPackageService _travelPackageService;
        private readonly IAccommodationService _accommodationService;
        private readonly IDestinationService _destinationService;
        private readonly IItineraryService _itineraryService;
        private readonly IShoppingCartService _shoppingCartService;

        public TravelPackagesController(ITravelPackageService travelPackageService, 
            IAccommodationService accommodationService, IDestinationService destinationService, IItineraryService itineraryService,
            IShoppingCartService shoppingCartService)
        {
            _travelPackageService = travelPackageService;
            _accommodationService = accommodationService;
            _destinationService = destinationService;
            _itineraryService = itineraryService;
            _shoppingCartService = shoppingCartService;
        }

        public IActionResult Index(string? searchQuery)
        {
            var packages = _travelPackageService.GetPackages();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                packages = packages.Where(p => p.DestinationInPackages
                    .Any(d => !string.IsNullOrEmpty(d.Destination.CityName) &&
                              d.Destination.CityName.Contains(searchQuery, StringComparison.OrdinalIgnoreCase) ||
                              !string.IsNullOrEmpty(d.Destination.CountryName) &&
                              d.Destination.CountryName.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)))
                    .ToList();

                ViewBag.SearchQuery = searchQuery;
            }
            return View(packages);
        }

        // GET: TravelPackages/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var travelPackage = _travelPackageService.GetPackages().Where(p => p.Id == id).First();
            if (travelPackage == null)
            {
                return NotFound();
            }

            return View(travelPackage);
        }

        // GET: TravelPackages/Create
        public IActionResult Create()
        {
            ViewBag.Accommodations = _accommodationService.GetAccommodations();
            ViewBag.Destinations = _destinationService.GetDestinations();
            ViewBag.Itineraries = _itineraryService.GetItineraries();
            return View();
        }

        // POST: TravelPackages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,Description,Image,DurationInDays,StartDate,EndDate,Price,Type,MaxSpots,TransportationDetails,Id")] TravelPackage travelPackage,
            List<Guid> accommodationIds, List<Guid> destinationIds, List<Guid> itinerariesIds)
        {
            List<AccommodationInPackage> accommodationInPackages1 = new List<AccommodationInPackage>();
            List<DestinationInPackage> destinationInPackages1 = new List<DestinationInPackage>();
            List<Itinerary> itineraries1 = new List<Itinerary>();
            if (ModelState.IsValid)
            {
                travelPackage.Id = Guid.NewGuid();

                var accommodationInPackages = accommodationIds.Select(accommodationId => new AccommodationInPackage
                {
                    AccommodationId = accommodationId,
                    PackageId = travelPackage.Id
                }).ToList();
                accommodationInPackages1.AddRange(accommodationInPackages);
                travelPackage.AccommodationInPackages = accommodationInPackages1;

                var destinationInPackages = destinationIds.Select(destinationId => new DestinationInPackage
                {
                    DestinationId = destinationId,
                    PackageId = travelPackage.Id
                }).ToList();
                destinationInPackages1.AddRange(destinationInPackages);
                travelPackage.DestinationInPackages = destinationInPackages1;

                var itinerariesList = _itineraryService.GetItineraries()
                    .Where(itinerary => itinerariesIds.Contains(itinerary.Id))
                    .ToList();
                foreach (var itinerary in itinerariesList)
                {
                    itinerary.PackageId = travelPackage.Id;
                }
                travelPackage.Itineraries = itinerariesList;

                travelPackage.Bookings = new List<Booking>();

                _travelPackageService.CreateNewPackage(travelPackage);
                return RedirectToAction(nameof(Index));
            }
            return View(travelPackage);
        }

  
        // GET: TravelPackages/Edit/5
        public IActionResult Edit(Guid? id)
        {
            ViewBag.Accommodations = _accommodationService.GetAccommodations();
            ViewBag.Destinations = _destinationService.GetDestinations();
            ViewBag.Itineraries = _itineraryService.GetItineraries();

            if (id == null)
            {
                return NotFound();
            }

            var travelPackage = _travelPackageService.GetPackageById(id);
            if (travelPackage == null)
            {
                return NotFound();
            }

            return View(travelPackage);
        }

        // POST: TravelPackages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Name,Description,Image,DurationInDays,StartDate,EndDate,Price,Type,MaxSpots,TransportationDetails,Id")] TravelPackage travelPackage
            , List<Guid> accommodationIds, List<Guid> destinationIds, List<Guid> itinerariesIds)
        {
            if (id != travelPackage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingPackage = _travelPackageService.GetPackageById(id);
                if (existingPackage == null)
                {
                    return NotFound();
                }
                existingPackage.Name = travelPackage.Name;
                existingPackage.Description = travelPackage.Description;
                existingPackage.Image = travelPackage.Image;
                existingPackage.DurationInDays = travelPackage.DurationInDays;
                existingPackage.StartDate = travelPackage.StartDate;
                existingPackage.EndDate = travelPackage.EndDate;
                existingPackage.Price = travelPackage.Price;
                existingPackage.Type = travelPackage.Type;
                existingPackage.MaxSpots = travelPackage.MaxSpots;
                existingPackage.TransportationDetails = travelPackage.TransportationDetails;
                existingPackage.AccommodationInPackages = accommodationIds.Select(accommodationId => new AccommodationInPackage
                {
                    AccommodationId = accommodationId,
                    PackageId = existingPackage.Id
                }).ToList();

                existingPackage.DestinationInPackages = destinationIds.Select(destinationId => new DestinationInPackage
                {
                    DestinationId = destinationId,
                    PackageId = existingPackage.Id
                }).ToList();

                existingPackage.Itineraries = _itineraryService.GetItineraries()
                    .Where(itinerary => itinerariesIds.Contains(itinerary.Id))
                    .ToList();

                _travelPackageService.UpdatePackage(existingPackage);

                return RedirectToAction(nameof(Index));
            }

            return View(travelPackage);
        }

        // GET: TravelPackages/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var travelPackage = _travelPackageService.GetPackages().Where(p => p.Id == id).First();
            if (travelPackage == null)
            {
                return NotFound();
            }

            return View(travelPackage);
        }

        // POST: TravelPackages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var travelPackage = _travelPackageService.GetPackageById(id);
            if (travelPackage != null)
            {
                _travelPackageService.DeletePackage(id);
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: TravelPackages/BookPackage
        [HttpPost]
        public IActionResult BookPackage(Guid id, int numberOfTravelers)
        {
            var package = _travelPackageService.GetPackages().AsQueryable()
                .Where(p => p.Id == id);
            var numberOfBookings = 0;
            if (User.Identity.IsAuthenticated)
            {
                foreach(var booking in package.ElementAt(0).Bookings)
                {
                    numberOfBookings += booking.NumberOfTravelers;
                }
                if(numberOfBookings + numberOfTravelers > package.ElementAt(0).MaxSpots)
                {
                    TempData["ErrorMessage"] = $"The package '{package.ElementAt(0).Name}' is fully booked!";
                    return RedirectToAction("Index");
                }
                else
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    _shoppingCartService.AddToCart(id, userId, numberOfTravelers);
                    _travelPackageService.UpdatePackage(package.ElementAt(0));
                    return RedirectToAction("Index", "ShoppingCarts");
                }
                
            }
            return NotFound();
        }

        private bool TravelPackageExists(Guid id)
        {
            return _travelPackageService.GetPackageById(id) != null;
        }
    }
}
