using Aow.Infrastructure.Paging;
using Aow.Services.Products;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebApp.API.Controllers
{
    //  [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet("api/ProductCategories/GetProductCategories")]
        public IActionResult GetProductCategories([FromQuery] PagingParameters pagingParameters, Guid categoryId, [FromServices] GetProducts getFinancialYears)
        {
            return Ok(getFinancialYears.Do(pagingParameters));
        }

        [HttpGet("api/Products/GetProducts")]
        public IActionResult GetProduct([FromQuery] PagingParameters pagingParameters, [FromServices] GetProducts getProduct)
        {
            return Ok(getProduct.Do(pagingParameters));
        }

        [HttpGet("api/Products/GetProductById")]
        public IActionResult GetProductById(Guid Id, [FromServices] GetProduct getProduct)
        {
            var product = getProduct.Do(Id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }


        [HttpPost("api/ProductCategories/AddFinancialYear")]
        public async Task<IActionResult> AddProduct([FromBody] AddProductCategory.AddProductCategoryRequest request, [FromServices] AddProductCategory addFinancialYear)
        {
            var response = await addFinancialYear.Do(request);

            if (!response.Success)
            {
                return BadRequest("Failed to add to cart");
            }
            return Ok(response);
        }

        [HttpPut("api/ProductCategories/UpdateFinancialYear")]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductCategory.UpdateProductCategoryRequest request, [FromServices] UpdateProductCategory updateFinancialYear)
        {
            var response = await updateFinancialYear.Do(request);

            if (response == null)
            {
                return BadRequest("Failed to update");
            }
            return Ok(response);
        }

        [HttpDelete("api/ProductCategories/DeleteProductCategory")]
        public async Task<IActionResult> DeleteProduct(Guid id, [FromServices] DeleteProductCategory deleteFinancialYear)
        {
            var result = await deleteFinancialYear.Do(id);
            if (!result.Success)
            {
                return BadRequest();
            }
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
