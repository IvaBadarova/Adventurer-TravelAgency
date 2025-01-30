using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Model;
using Microsoft.EntityFrameworkCore;
using TravelAgency.Domain.Domain;
using TravelAgency.Repository;
using TravelAgency.Service.Interface;

namespace TravelAgency.Web.Controllers
{
    public class ItinerariesController : Controller
    {
        private readonly IItineraryService _itineraryService;
        private readonly IActivityService _activityService;
        private readonly ITravelPackageService _travelPackageService;

        public ItinerariesController(IItineraryService itineraryService, IActivityService activityService, ITravelPackageService travelPackageService)
        {
            _itineraryService = itineraryService;
            _activityService = activityService;
            _travelPackageService = travelPackageService;
        }

        // GET: Itineraries
        public IActionResult Index()
        {
            return View(_itineraryService.GetItineraries());
        }

        // GET: Itineraries/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itinerary = _itineraryService.GetItineraries().Where(i => i.Id == id).First();
            if (itinerary == null)
            {
                return NotFound();
            }

            return View(itinerary);
        }

        // GET: Itineraries/Create
        public IActionResult Create()
        {
            ViewBag.Activities = _activityService.GetActivities();
            ViewBag.Packages = new SelectList(_travelPackageService.GetPackages(), "Id", "Name");
            return View();
        }

        // POST: Itineraries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Day,Title,Description,Location,TransportationDetails,AccomodationDetails,PackageId,Id")] Itinerary itinerary
            ,List<Guid> activitiesIds)
        {
            List<ActivityInItinerary> activityInItineraries1 = new List<ActivityInItinerary>();
            if (ModelState.IsValid)
            {
                itinerary.Id = Guid.NewGuid();

                var activityInItineraries = activitiesIds.Select(activityId => new ActivityInItinerary
                {
                    ActivityId = activityId,
                    ItineraryId = itinerary.Id,
                    Activity = _activityService.GetActivityById(activityId),
                    Itinerary = itinerary
                }).ToList();
                activityInItineraries1.AddRange(activityInItineraries);
                itinerary.ActivityInItineraries = activityInItineraries1;
                itinerary.TravelPackage = _travelPackageService.GetPackageById(itinerary.PackageId);
                _itineraryService.CreateNewItinerary(itinerary);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Packages = new SelectList(_travelPackageService.GetPackages(), "Id", "Name", itinerary.PackageId);
            return View(itinerary);
        }

        // GET: Itineraries/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itinerary = _itineraryService.GetItineraryById(id);
            if (itinerary == null)
            {
                return NotFound();
            }

            ViewBag.Activities = _activityService.GetActivities();
            ViewData["PackageId"] = new SelectList(_travelPackageService.GetPackages(), "Id", "Name");

            return View(itinerary);
        }

        // POST: Itineraries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Day,Title,Description,Location,TransportationDetails,AccomodationDetails,PackageId,Id")] Itinerary itinerary
            , List<Guid> activitiesIds)
        {
            List<ActivityInItinerary> activityInItineraries1 = new List<ActivityInItinerary>();

            if (id != itinerary.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var activityInItineraries = activitiesIds.Select(activityId => new ActivityInItinerary
                {
                    ActivityId = activityId,
                    ItineraryId = itinerary.Id,
                    Activity = _activityService.GetActivityById(activityId),
                    Itinerary = itinerary
                }).ToList();
                activityInItineraries1.AddRange(activityInItineraries);
                itinerary.ActivityInItineraries = activityInItineraries1;
                itinerary.TravelPackage = _travelPackageService.GetPackageById(itinerary.PackageId);

                _itineraryService.UpdateItinerary(itinerary);
                return RedirectToAction(nameof(Index));
            }
            ViewData["PackageId"] = new SelectList(_travelPackageService.GetPackages(), "Id", "Name", itinerary.PackageId);

            return View(itinerary);
        }

        // GET: Itineraries/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itinerary = _itineraryService.GetItineraries().Where(i => i.Id == id).First();
            if (itinerary == null)
            {
                return NotFound();
            }

            return View(itinerary);
        }

        // POST: Itineraries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var itinerary = _itineraryService.GetItineraryById(id);
            if (itinerary != null)
            {
                _itineraryService.DeleteItinerary(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ItineraryExists(Guid id)
        {
            return _itineraryService.GetItineraryById(id) != null;
        }
    }
}
