using System.Data;

using ZdzTest_Models;

namespace ZdzTest_Repositories
{
    public class DeveloperRepository : BaseRepository<Developer>, IDeveloperRepository
    {
        public DeveloperRepository(IDbConnection dbConnection) : base(dbConnection)
        {
        }
    }
}
