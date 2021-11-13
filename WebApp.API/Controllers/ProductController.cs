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
        [HttpGet("api/Products/GetProducts")]
        public IActionResult GetProducts([FromQuery] PagingParameters pagingParameters, Guid cmpId, [FromServices] GetProducts getFinancialYears)
        {
            return Ok(getFinancialYears.Do(pagingParameters, cmpId));
        }

        [HttpGet("api/Products/GetProduct")]
        public IActionResult GetProduct([FromQuery] Guid id, [FromServices] GetProduct getProduct)
        {
            var result = getProduct.Do(id);
            return Ok(result);
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

        [HttpPost("api/Products/AddProduct")]
        public async Task<IActionResult> AddProduct([FromBody] AddProduct.AddProductRequest request, [FromServices] AddProduct addFinancialYear)
        {
            var response = await addFinancialYear.Do(request);

            if (!response.Success)
            {
                return BadRequest("Failed to add to cart");
            }
            return Ok(response);
        }

        [HttpPut("api/Products/UpdateProduct")]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProduct.UpdateProductRequest request, [FromServices] UpdateProduct updateFinancialYear)
        {
            var response = await updateFinancialYear.Do(request);

            if (response == null)
            {
                return BadRequest("Failed to update");
            }
            return Ok(response);
        }

        [HttpDelete("api/Products/DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(Guid id, [FromServices] DeleteProduct deleteFinancialYear)
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
