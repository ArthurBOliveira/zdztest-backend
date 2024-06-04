using System.Data;

using ZdzTest_Models;

namespace ZdzTest_Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(IDbConnection dbConnection) : base(dbConnection)
        {
        }
    }
}
