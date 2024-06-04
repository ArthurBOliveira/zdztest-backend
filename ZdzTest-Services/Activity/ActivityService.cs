using ZdzTest_Models;
using ZdzTest_Repositories;

namespace ZdzTest_Services
{
    public class ActivityService : BaseService<Activity>, IActivityService
    {
        public ActivityService(IActivityRepository repository) : base(repository)
        {
        }
    }
}
