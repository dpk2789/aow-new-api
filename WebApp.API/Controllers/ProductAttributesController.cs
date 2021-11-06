using Aow.Infrastructure.Paging;
using Aow.Services.ProductAttribute;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebApp.API.Controllers
{
    // [Route("api/[controller]")]
    [ApiController]
    public class ProductAttributesController : ControllerBase
    {
        [HttpGet("api/ProductAttributes/GetProductAttributes")]
        public IActionResult GetProductAttributes([FromQuery] PagingParameters pagingParameters, Guid categoryId, [FromServices] GetProductAttributes getProductCategories)
        {
            var result = getProductCategories.Do(pagingParameters, categoryId);
            return Ok(result);
        }

        [HttpGet("api/ProductAttributes/GetProductAttribute")]
        public IActionResult GetProductAttribute([FromQuery] Guid id, [FromServices] GetProductAttribute getProduct)
        {
            var result = getProduct.Do(id);
            return Ok(result);
        }

        [HttpPost("api/ProductAttributes/AddProductAttribute")]
        public async Task<IActionResult> AddProductAttribute([FromBody] AddProductAttribute.AddProductAttributeRequest request, [FromServices] AddProductAttribute addFinancialYear)
        {
            var response = await addFinancialYear.Do(request);

            if (!response.Success)
            {
                return BadRequest("Failed to add to cart");
            }
            return Ok(response);
        }

        [HttpPut("api/ProductAttributes/UpdateProductAttribute")]
        public async Task<IActionResult> UpdateProductAttribute([FromBody] UpdateProductAttibute.UpdateProductAttributeRequest request, [FromServices] UpdateProductAttibute updateProductAttribute)
        {
            var response = await updateProductAttribute.Do(request);

            if (!response.Success)
            {
                return BadRequest("Failed to add to cart");
            }
            return Ok(response);
        }
    }
}
