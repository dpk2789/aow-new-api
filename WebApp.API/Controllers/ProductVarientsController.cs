using Aow.Infrastructure.Paging;
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
        [HttpGet("api/ProductVarients/GetProductVarients")]
        public IActionResult GetProductVarients([FromQuery] PagingParameters pagingParameters, Guid productId, [FromServices] GetProductVariants getProductVariants)
        {
            var result = getProductVariants.Do(pagingParameters, productId);
            return Ok(result);
        }

        [HttpGet("api/ProductVarients/GetProductVarient")]
        public IActionResult GetProductVarient([FromQuery] Guid id, [FromServices] GetProductVariant getProduct)
        {
            var result = getProduct.Do(id);
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
    }
}
