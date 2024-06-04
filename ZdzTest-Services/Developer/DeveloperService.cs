using ZdzTest_Models;
using ZdzTest_Repositories;

namespace ZdzTest_Services
{
    public class DeveloperService : BaseService<Developer>, IDeveloperService
    {
        public DeveloperService(IDeveloperRepository repository) : base(repository)
        {
        }
    }

}
