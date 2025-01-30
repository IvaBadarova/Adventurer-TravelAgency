using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Domain;

namespace TravelAgency.Service.Interface
{
    public interface ITravelPackageService
    {
        public List<TravelPackage> GetPackages();
        public TravelPackage GetPackageById(Guid? id);
        public TravelPackage CreateNewPackage(TravelPackage package);
        public TravelPackage UpdatePackage(TravelPackage package);
        public TravelPackage DeletePackage(Guid id);

    }
}
