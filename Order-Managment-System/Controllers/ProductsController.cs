using AutoMapper;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order_Managment_System.Dtos;
using Order_Managment_System.ErrorResponse;
using Services;

namespace Order_Managment_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductServices productServices;
        private readonly IMapper mapper;

        public ProductsController(IProductServices productServices ,IMapper mapper)
        {
            this.productServices = productServices;
            this.mapper = mapper;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("products")]
        public async Task<ActionResult<ProductDto>> AddNewProduct(ProductDto productDto)
        {
            var product = mapper.Map<ProductDto,Product>(productDto);
            await productServices.AddProductAsync(product);
            var mappedProduct = mapper.Map<Product,ProductDto>(product);
            if (product == null)
            {
              return NotFound(new ApiException(404 , "This Product Not Found"));
            }
            return Ok(mappedProduct);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("products/{productId}")]
        public async Task<ActionResult> UpdateProduct(Product productDto, int ProductId)
        {
            if (ProductId != productDto.Id)
                return BadRequest(new ApiException(500,"Bad Request"));

            var productResult = await productServices.GetProductByIdAsync(ProductId);

            if (productDto is null)
                return NotFound(new ApiException(404, "This Product Not Found"));


            var result = await productServices.UpdateProductAsync(productDto);
            if (result == null)
                return BadRequest(new ApiException(500, "Faild to Update the Product"));

            return Ok(result);
        }
        [HttpGet("AllProducts")]
        public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetAllProducts()
        {
          
            var products = await productServices.GetAllProductsAsync();
            var mappedProducts = mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductDto>>(products);
            if (products.Count== 0)
            {
                return NotFound(new ApiException(404, "No Products Found"));

            }
            return Ok(mappedProducts);
        }
        [HttpGet("product/{productId}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var product = await productServices.GetProductByIdAsync(id);
            var mappedProduct = mapper.Map<Product,ProductDto>(product);
            if (product == null)
            {
                return NotFound(new ApiException(404, "This Product Not Found"));

            }
            return Ok(mappedProduct);
        }
    }
}
