using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Domain;

namespace TravelAgency.Service.Interface
{
    public interface IActivityService
    {
        public List<Activity> GetActivities();
        public Activity GetActivityById(Guid? id);
        public Activity CreateNewActivity(Activity activity);
        public Activity UpdateActivity(Activity activity);
        public Activity DeleteActivity(Guid id);
    }
}
