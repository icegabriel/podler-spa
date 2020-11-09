using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Podler.Data;
using Podler.Models;

namespace Podler.Repositories
{
    public class StaffsRepository : RepositoryBase<Staff>, IStaffsRepository
    {
        public StaffsRepository(ApplicationContext Context) : base(Context)
        {
        }

        public async Task<Staff> GetByTitleAsync(string title)
        {
            return await DbSet.Where(s => s.Title.ToUpper() == title.ToUpper()).SingleOrDefaultAsync();
        }
    }

    public interface IStaffsRepository : IRepositoryBase<Staff>
    {
        Task<Staff> GetByTitleAsync(string title);
    }
}