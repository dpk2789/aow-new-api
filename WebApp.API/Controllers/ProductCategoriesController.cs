using Aow.Infrastructure.Paging;
using Aow.Services.ProductCategory;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebApp.API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]

    public class ProductCategoriesController : ControllerBase
    {
        [HttpGet("api/ProductCategories/ProductCategories")]
        public IActionResult GetFinancialYears([FromQuery] PagingParameters pagingParameters, Guid cmpId, [FromServices] GetProductCategories getFinancialYears)
        {
            return Ok(getFinancialYears.Do(pagingParameters, cmpId));
        }

        [HttpGet("api/ProductCategories/GetProductCategory")]
        public IActionResult GetFinancialYear([FromQuery] Guid id, [FromServices] GetProductCategory getProduct)
        {
            var result = getProduct.Do(id);
            return Ok(result);
        }

        [HttpPost("api/ProductCategories/AddProductCategory")]
        public async Task<IActionResult> AddProductCategory([FromBody] AddProductCategory.AddProductCategoryRequest request, [FromServices] AddProductCategory addFinancialYear)
        {
            var response = await addFinancialYear.Do(request);

            if (!response.Success)
            {
                return BadRequest("Failed to add to cart");
            }
            return Ok(response);
        }

        [HttpPut("api/ProductCategories/UpdateProductCategory")]
        public async Task<IActionResult> UpdateProductCategory([FromBody] UpdateProductCategory.UpdateProductCategoryRequest request, [FromServices] UpdateProductCategory updateFinancialYear)
        {
            var response = await updateFinancialYear.Do(request);

            if (response == null)
            {
                return BadRequest("Failed to update");
            }
            return Ok(response);
        }

        [HttpDelete("api/ProductCategories/DeleteProductCategory")]
        public async Task<IActionResult> DeleteProductCategory(Guid id, [FromServices] DeleteProductCategory deleteFinancialYear)
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
