using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Domain;

namespace TravelAgency.Service.Interface
{
    public interface IAccommodationService
    {
        public List<Accommodation> GetAccommodations();
        public Accommodation GetAccommodationById(Guid? id);
        public Accommodation CreateNewAccommodation(Accommodation accommodation);
        public Accommodation UpdateAccommodation(Accommodation accommodation);
        public Accommodation DeleteAccommodation(Guid id);
    }
}
