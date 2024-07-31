using Core.Reposatries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reposatry.Data
{
    public class GenaricReposartry<T> : IGenaricReposatry<T> where T : class
    {
        private readonly StoreDbContext dbContext;

        public GenaricReposartry(StoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public  async Task AddAsync(T entity)
            => await dbContext.AddAsync(entity);   
        
        public   void DeleteAsync(T entity)
         =>   dbContext.Remove(entity);

        public async Task<IReadOnlyList<T>> GetAllSpecAsync()
        {
            return await dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdSpecAsync(int Id)
        {
            return await dbContext.Set<T>().FirstOrDefaultAsync();
        }

        public void UpdateAsync(T entity)
         => dbContext.Update(entity);
    }
}
