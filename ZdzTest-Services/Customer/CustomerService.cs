using ZdzTest_Models;
using ZdzTest_Repositories;

namespace ZdzTest_Services
{
    public class CustomerService : BaseService<Customer>, ICustomerService
    {
        public CustomerService(ICustomerRepository repository) : base(repository)
        {
        }
    }

}
