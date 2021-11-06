using Aow.Services.ProductAttribute;
using Aow.Services.ProductAttributeOptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebApp.API.Controllers
{
    // [Route("api/[controller]")]
    [ApiController]
    public class ProductAttributeOptionController : ControllerBase
    {
        [HttpGet("api/ProductAttributeOption/GetProductAttributeOption")]
        public IActionResult GetProductAttributeOption([FromQuery] Guid id, [FromServices] GetProductAttributeOption getProduct)
        {
            var result = getProduct.Do(id);
            return Ok(result);
        }

        [HttpPost("api/ProductAttributeOption/AddProductAttributeOption")]
        public async Task<IActionResult> AddProductAttributeOption([FromBody] AddProductAttribute.AddProductAttributeRequest request, [FromServices] AddProductAttribute addFinancialYear)
        {
            var response = await addFinancialYear.Do(request);

            if (!response.Success)
            {
                return BadRequest("Failed to add to cart");
            }
            return Ok(response);
        }

        [HttpPut("api/ProductAttributeOption/UpdateProductAttributeOption")]
        public async Task<IActionResult> UpdateProductAttributeOption([FromBody] UpdateProductAttibute.UpdateProductAttributeRequest request, [FromServices] UpdateProductAttibute updateProductAttribute)
        {
            var response = await updateProductAttribute.Do(request);

            if (!response.Success)
            {
                return BadRequest("Failed to add to cart");
            }
            return Ok(response);
        }

        [HttpDelete("api/ProductAttributeOption/DeleteProductAttributeOption")]
        public async Task<IActionResult> DeleteProductAttributeOption(Guid id, [FromServices] DeleteProductAttibuteOption deleteProduct)
        {
            var result = await deleteProduct.Do(id);
            if (result == null)
            {
                return NotFound();
            }
            if (!result.Success)
            {
                return BadRequest();
            }          
            return Ok(result);
        }
    }
}
