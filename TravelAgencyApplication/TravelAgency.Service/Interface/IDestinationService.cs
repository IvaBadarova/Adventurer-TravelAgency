using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Domain;

namespace TravelAgency.Service.Interface
{
    public interface IDestinationService
    {
        public List<Destination> GetDestinations();
        public Destination GetDestinationById(Guid? id);
        public Destination CreateNewDestination(Destination destination);
        public Destination UpdateDestination(Destination destination);
        public Destination DeleteDestination(Guid id);
    }
}
