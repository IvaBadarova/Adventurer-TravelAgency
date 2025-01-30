using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Domain;
using TravelAgency.Repository.Interface;
using TravelAgency.Service.Interface;

namespace TravelAgency.Service.Implementation
{
    public class ItineraryService : IItineraryService
    {
        private readonly IRepository<Itinerary> _itineraryRepository;

        public ItineraryService(IRepository<Itinerary> itineraryRepository)
        {
            _itineraryRepository = itineraryRepository;
        }
        public Itinerary CreateNewItinerary(Itinerary itinerary)
        {
            return _itineraryRepository.Insert(itinerary);
        }

        public Itinerary DeleteItinerary(Guid id)
        {
            var itinerary = _itineraryRepository.Get(id);
            return _itineraryRepository.Delete(itinerary);
        }

        public List<Itinerary> GetItineraries()
        {
            return _itineraryRepository.GetAll().ToList();
        }

        public Itinerary GetItineraryById(Guid? id)
        {
            return _itineraryRepository.Get(id);
        }

        public Itinerary UpdateItinerary(Itinerary itinerary)
        {
            return _itineraryRepository.Update(itinerary);
        }
    }
}
