using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Podler.Data;
using Podler.Models;

namespace Podler.Repositories
{
    public class GenresRepository : RepositoryBase<Genre>, IGenresRepository
    {
        public GenresRepository(ApplicationContext Context) : base(Context)
        {
        }

        public async Task<Genre> GetByTitleAsync(string title)
        {
            return await DbSet.Where(g => g.Title.ToUpper() == title.ToUpper()).SingleOrDefaultAsync();
        }
    }

    public interface IGenresRepository : IRepositoryBase<Genre>
    {
        Task<Genre> GetByTitleAsync(string title);
    }
}
