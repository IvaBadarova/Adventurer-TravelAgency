using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TravelAgency.Domain.Domain;
using TravelAgency.Repository;
using TravelAgency.Service.Implementation;
using TravelAgency.Service.Interface;

namespace TravelAgency.Web.Controllers
{
    public class DestinationsController : Controller
    {
        private readonly IDestinationService _destinationService;

        public DestinationsController(IDestinationService destinationService)
        {
             _destinationService = destinationService;
        }

        // GET: Destinations
        public IActionResult Index()
        {
            return View(_destinationService.GetDestinations());
        }

        // GET: Destinations/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var destination = _destinationService.GetDestinationById(id);
            if (destination == null)
            {
                return NotFound();
            }

            return View(destination);
        }

        // GET: Destinations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Destinations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("CityName,CountryName,Description,Image,Id")] Destination destination)
        {
            if (ModelState.IsValid)
            {
                destination.Id = Guid.NewGuid();
                _destinationService.CreateNewDestination(destination);
                return RedirectToAction(nameof(Index));
            }
            return View(destination);
        }

        // GET: Destinations/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var destination = _destinationService.GetDestinationById(id);
            if (destination == null)
            {
                return NotFound();
            }
            return View(destination);
        }

        // POST: Destinations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("CityName,CountryName,Description,Image,Id")] Destination destination)
        {
            if (id != destination.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _destinationService.UpdateDestination(destination);
                return RedirectToAction(nameof(Index));
            }
            return View(destination);
        }

        // GET: Destinations/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var destination = _destinationService.GetDestinationById(id);
            if (destination == null)
            {
                return NotFound();
            }

            return View(destination);
        }

        // POST: Destinations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var destination = _destinationService.GetDestinationById(id);
            if (destination != null)
            {
                _destinationService.DeleteDestination(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool DestinationExists(Guid id)
        {
            return _destinationService.GetDestinationById(id) != null;
        }
    }
}
