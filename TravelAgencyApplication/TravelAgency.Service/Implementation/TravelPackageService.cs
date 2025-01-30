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
    public class TravelPackageService : ITravelPackageService
    {
        private readonly IRepository<TravelPackage> _travelPackageRepository;

        public TravelPackageService(IRepository<TravelPackage> travelPackageRepository)
        {
            _travelPackageRepository = travelPackageRepository;
        }

        public TravelPackage CreateNewPackage(TravelPackage package)
        {
            return _travelPackageRepository.Insert(package);
        }

        public TravelPackage DeletePackage(Guid id)
        {
            var package = _travelPackageRepository.Get(id);
            return _travelPackageRepository.Delete(package);
        }

        public TravelPackage GetPackageById(Guid? id)
        {
            return _travelPackageRepository.Get(id);
        }

        public List<TravelPackage> GetPackages()
        {
            return _travelPackageRepository.GetAll().ToList();
        }

        public TravelPackage UpdatePackage(TravelPackage package)
        {
            return _travelPackageRepository.Update(package);
        }
    }
}
