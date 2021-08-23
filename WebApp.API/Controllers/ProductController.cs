using Aow.Infrastructure.Domain;
using Aow.Infrastructure.Paging;
using Aow.Infrastructure.Repositories;
using Aow.Services.Products;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Aow.Services.Products.GetProducts;

namespace WebApp.API.Controllers
{
    //  [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet("api/Products/GetProducts")]
        public IActionResult GetProduct([FromQuery] PagingParameters pagingParameters, [FromServices] GetProducts getProduct)
        {
            return Ok(getProduct.Do(pagingParameters));
        }

        [HttpGet("api/Products/GetProductByName")]
        public IActionResult GetProductById(Guid Id)
        {
            var product = _productRepository.GetProduct(Id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }


    }
}
