using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Podler.Data;
using Podler.Models;

namespace Podler.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : ModelBase
    {
        protected ApplicationContext Context { get; }

        protected DbSet<T> DbSet { get => Context.Set<T>(); }
        
        public RepositoryBase(ApplicationContext Context)
        {
            this.Context = Context;
        }

        public virtual async Task<T> GetAsync(int id)
        {
            return await DbSet.Where(obj => obj.Id == id).FirstOrDefaultAsync();
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public virtual async Task<List<T>> FindAsync(params int[] ps)
        {
            var objs = new List<T>();

            foreach (var id in ps)
                objs.Add(await GetAsync(id));

            return objs;
        }

        public virtual async Task IncludeAsync(T obj)
        {
            await DbSet.AddAsync(obj);

            await Context.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(T obj)
        {
            DbSet.Update(obj);

            await Context.SaveChangesAsync();
        }

        public virtual async Task RemoveAsync(T obj)
        {
            DbSet.Remove(obj);
            await Context.SaveChangesAsync();
        }
    }

    public interface IRepositoryBase<T> where T : ModelBase
    {
        Task<T> GetAsync(int id);
        Task<List<T>> GetAllAsync();
        Task<List<T>> FindAsync(params int[] ps);
        Task IncludeAsync(T obj);
        Task UpdateAsync(T obj);
        Task RemoveAsync(T obj);
    }
}
