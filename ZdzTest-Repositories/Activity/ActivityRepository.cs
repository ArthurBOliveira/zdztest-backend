using System.Data;

using ZdzTest_Models;

namespace ZdzTest_Repositories
{
    public class ActivityRepository : BaseRepository<Activity>, IActivityRepository
    {
        public ActivityRepository(IDbConnection dbConnection) : base(dbConnection)
        {
        }
    }
}