using Aow.Infrastructure.IRepositories;
using System;
using System.Threading.Tasks;

namespace Aow.Services.Products
{
    [Service]
    public class UpdateProduct
    {
        private IRepositoryWrapper _repoWrapper;
        public UpdateProduct(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        } 
        public class UpdateProductRequest
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public Guid ProductCategoryId { get; set; }
        }
        public class UpdateProductResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public bool Success { get; set; }
        }

        public async Task<UpdateProductResponse> Do(UpdateProductRequest request)
        {
            try
            {
                var product = _repoWrapper.ProductRepo.GetProduct(request.Id);
                if (product == null)
                {
                    return null;
                }
                product.Name = request.Name;
                product.ProductCategoryId = request.ProductCategoryId;
                _repoWrapper.ProductRepo.Update(product);

                int i = await _repoWrapper.SaveNew();
                if (i <= 0)
                {
                    return new UpdateProductResponse
                    {
                        Name = request.Name,
                        Success = false,
                        Description = "Some Error Occur"
                    };
                }
                else
                {
                    return new UpdateProductResponse
                    {
                        Id = product.Id,
                        Name = request.Name,
                        Success = true,
                        Description = "Successfully Updated Product!!"
                    };
                }
            }
            catch (Exception ex)
            {
                return new UpdateProductResponse
                {
                    Name = request.Name,
                    Success = false,
                    Description = ex.Message
                };
            }
        }
    }
}
