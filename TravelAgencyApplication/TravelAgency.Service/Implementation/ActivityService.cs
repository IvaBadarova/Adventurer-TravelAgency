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
    public class ActivityService : IActivityService
    {
        private readonly IRepository<Activity> _activityRepository;
        public ActivityService(IRepository<Activity> activityRepository)
        {
            _activityRepository = activityRepository;
        }

        public Activity CreateNewActivity(Activity activity)
        {
            return _activityRepository.Insert(activity);
        }

        public Activity DeleteActivity(Guid id)
        {
            var activity = _activityRepository.Get(id);
            return _activityRepository.Delete(activity);
        }

        public List<Activity> GetActivities()
        {
            return _activityRepository.GetAll().ToList();
        }

        public Activity GetActivityById(Guid? id)
        {
            return _activityRepository.Get(id);
        }

        public Activity UpdateActivity(Activity activity)
        {
            return _activityRepository.Update(activity);
        }
    }
}
