using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Reposatries
{
    public interface IGenaricReposatry<T> where T : class
    {
        Task<IReadOnlyList<T>> GetAllSpecAsync();
        Task<T> GetByIdSpecAsync(int Id);
        Task AddAsync(T entity);
        void  UpdateAsync(T entity);    
        void DeleteAsync(T entity);


    }
}
