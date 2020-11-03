using System;
using Podler.Data;
using Podler.Models;

namespace Podler.Repositories
{
    public class GenresRepository : RepositoryBase<Genre>, IGenresRepository
    {
        public GenresRepository(ApplicationContext Context) : base(Context)
        {
        }
    }

    public interface IGenresRepository : IRepositoryBase<Genre>
    {
    }
}
