using Core;
using Core.Reposatries;
using Reposatry.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reposatry
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext dbContext;
        private Hashtable Reposatry;
        public UnitOfWork(StoreDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.Reposatry = new Hashtable();
        }
        public async Task<int> CompleteAsync()
         => await dbContext.SaveChangesAsync();

        public async ValueTask DisposeAsync()
        =>await dbContext.DisposeAsync();

        public IGenaricReposatry<TEntity> Repository<TEntity>() where TEntity : class
        {
            var Key = typeof(TEntity).Name;
            if(!Reposatry.ContainsKey(Key)) 
            { 
                var repository = new GenaricReposartry<TEntity>(dbContext);

                Reposatry.Add(Key, repository);
            
            
            }
            return Reposatry[Key] as IGenaricReposatry<TEntity>;
        }
    }
}
