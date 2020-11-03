using System;
using Podler.Data;
using Podler.Models;

namespace Podler.Repositories
{
    public class ThemesRepository : RepositoryBase<Theme>, IThemesRepository
    {
        public ThemesRepository(ApplicationContext Context) : base(Context)
        {
        }
    }

    public interface IThemesRepository : IRepositoryBase<Theme>
    {
    }
}
