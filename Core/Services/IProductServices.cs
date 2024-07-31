using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IProductServices
    {
        Task<Product> AddProductAsync(Product product); //Admin
        Task<Product> UpdateProductAsync(Product product); //Admin
        Task<Product> GetProductByIdAsync(int id); //User
        Task<IReadOnlyList<Product>> GetAllProductsAsync();
        //Task<IReadOnlyList<Product>> GetProductsByCategoryAsync(int categoryId);
        //Task<IReadOnlyList<Product>> GetProductsByBrandAsync(int brandId);
    }
}
