using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Domain;

namespace TravelAgency.Service.Interface
{
    public interface IItineraryService
    {
        public List<Itinerary> GetItineraries();
        public Itinerary GetItineraryById(Guid? id);
        public Itinerary CreateNewItinerary(Itinerary itinerary);
        public Itinerary UpdateItinerary(Itinerary itinerary);
        public Itinerary DeleteItinerary(Guid id);
    }
}
