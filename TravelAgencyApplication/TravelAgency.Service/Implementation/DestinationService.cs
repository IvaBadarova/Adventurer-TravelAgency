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
    public class DestinationService : IDestinationService
    {
        private readonly IRepository<Destination> _destinationRepository;

        public DestinationService(IRepository<Destination> destinationRepository)
        {
            _destinationRepository = destinationRepository;
        }
        public Destination CreateNewDestination(Destination destination)
        {
            return _destinationRepository.Insert(destination);
        }

        public Destination DeleteDestination(Guid id)
        {
            var destination = _destinationRepository.Get(id);
            return _destinationRepository.Delete(destination);
        }

        public Destination GetDestinationById(Guid? id)
        {
            return _destinationRepository.Get(id);
        }

        public List<Destination> GetDestinations()
        {
            return _destinationRepository.GetAll().ToList();
        }

        public Destination UpdateDestination(Destination destination)
        {
            return _destinationRepository.Update(destination);
        }
    }
}
