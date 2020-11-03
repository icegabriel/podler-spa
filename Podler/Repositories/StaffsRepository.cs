using Podler.Data;
using Podler.Models;

namespace Podler.Repositories
{
    public class StaffsRepository : RepositoryBase<Staff>, IStaffsRepository
    {
        public StaffsRepository(ApplicationContext Context) : base(Context)
        {
        }
    }

    public interface IStaffsRepository : IRepositoryBase<Staff>
    {

    }
}