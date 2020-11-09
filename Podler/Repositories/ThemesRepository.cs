using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Podler.Data;
using Podler.Models;

namespace Podler.Repositories
{
    public class ThemesRepository : RepositoryBase<Theme>, IThemesRepository
    {
        public ThemesRepository(ApplicationContext Context) : base(Context)
        {
        }

        public async Task<Theme> GetByTitleAsync(string title)
        {
            return await DbSet.Where(g => g.Title.ToUpper() == title.ToUpper()).SingleOrDefaultAsync();
        }
    }

    public interface IThemesRepository : IRepositoryBase<Theme>
    {
        Task<Theme> GetByTitleAsync(string title);
    }
}
