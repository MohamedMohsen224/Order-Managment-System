using Core;
using Core.Models;
using Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductServices : IProductServices
    {
        private readonly IUnitOfWork unitOfWork;

        public ProductServices(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Product> AddProductAsync(Product product)
        {
             await unitOfWork.Repository<Product>().AddAsync(product);
            var Product = await unitOfWork.CompleteAsync();

            if(Product <= 0 )
                return null;

            return product;
           
        }

        public async Task<IReadOnlyList<Product>> GetAllProductsAsync()
        {
            return await unitOfWork.Repository<Product>().GetAllSpecAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await unitOfWork.Repository<Product>().GetByIdSpecAsync(id);
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            unitOfWork.Repository<Product>().UpdateAsync(product);
            var result = await unitOfWork.CompleteAsync();
             if(result <= 0)
                return null;
             return product;
        }
    }
}
