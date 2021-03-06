using Aow.Infrastructure.Paging;
using Aow.Services.ProductAttributeOptions;
using Aow.Services.ProductVariants;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebApp.API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class ProductVarientsController : ControllerBase
    {
        [HttpGet("api/ProductVarients/GetProductAttributeOptionsByProduct")]
        public async Task<IActionResult> GetProductAttributeOptionsByProduct(Guid productId, [FromServices] GetProductAttributeOptionsByProduct getAttributeOptions)
        {
            var result = await getAttributeOptions.Do(productId);
            return Ok(result);
        }

        [HttpGet("api/ProductVarients/GetProductVarients")]
        public IActionResult GetProductVarients([FromQuery] PagingParameters pagingParameters, Guid productId, [FromServices] GetProductVariants getProductVariants)
        {
            var result = getProductVariants.Do(pagingParameters, productId);
            return Ok(result);
        }

        [HttpGet("api/ProductVarients/GetAllVarientsByProduct")]
        public IActionResult GetAllVarientsByProduct(Guid productId, [FromServices] GetAllVarientsByProduct getProductVariants)
        {
            var result = getProductVariants.Do(productId);
            return Ok(result);
        }

        [HttpGet("api/ProductVarients/GetAllVarientsByOption")]
        public async Task<IActionResult> GetAllVarientsByOption(string data, [FromServices] GetVariantResultByAttOption getProductVariants)
        {
            var result = await getProductVariants.Do(data);
            return Ok(result);
        }

        [HttpGet("api/ProductVarients/GetAllVarientsByCompany")]
        public IActionResult GetAllVarientsByCompany(Guid companyId, [FromServices] GetAllVarientsByCompany getProductVariants)
        {
            var result = getProductVariants.Do(companyId);
            return Ok(result);
        }

        [HttpGet("api/ProductVarients/GetProductVarient")]
        public async Task<IActionResult> GetProductVarient([FromQuery] Guid id, [FromServices] GetProductVariant getProduct)
        {
            var result = await getProduct.Do(id);
            return Ok(result);
        }

        [HttpPost("api/ProductVarients/AddProductVarient")]
        public async Task<IActionResult> AddProductVarient([FromBody] AddProductVariant.AddProductVariantRequest request, [FromServices] AddProductVariant addProductVariant)
        {
            var response = await addProductVariant.Do(request);

            if (!response.Success)
            {
                return BadRequest("Failed to add to cart");
            }
            return Ok(response);
        }

        [HttpPut("api/ProductVarients/UpdateProductVarient")]
        public async Task<IActionResult> UpdateProductVarient([FromBody] UpdateProductVariant.UpdateProductVariantRequest request, [FromServices] UpdateProductVariant updateProductAttribute)
        {
            var response = await updateProductAttribute.Do(request);

            if (!response.Success)
            {
                return BadRequest("Failed to add to cart");
            }
            return Ok(response);
        }

        [HttpDelete("api/ProductVarients/DeleteProductVarient")]
        public async Task<IActionResult> DeleteProductVarient(Guid id, [FromServices] DeleteProductVariant deleteProduct)
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
