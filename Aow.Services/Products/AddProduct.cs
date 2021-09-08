using Aow.Infrastructure.IRepositories;
using System;
using System.Threading.Tasks;

namespace Aow.Services.Products
{
    [Service]
    public class AddProduct
    {
        private IRepositoryWrapper _repoWrapper;
        public AddProduct(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class AddProductRequest
        {
            public string Name { get; set; }
            public string CategoryId { get; set; }
        }
        public class AddProductResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public bool Success { get; set; }
        }
        public async Task<AddProductResponse> Do(AddProductRequest request)
        {
            try
            {
                Guid ProductId = Guid.NewGuid();
                Guid categoryId = Guid.Parse(request.CategoryId);
                var product = new Aow.Infrastructure.Domain.Product
                {
                    Id = ProductId,
                    Name = request.Name,
                    ProductCategoryId = categoryId
                };

                _repoWrapper.ProductRepo.Create(product);
                int i = await _repoWrapper.SaveNew();
                if (i <= 0)
                {
                    return new AddProductResponse
                    {
                        Name = request.Name,
                        Success = false
                    };
                }
                else
                {
                    return new AddProductResponse
                    {
                        Id = product.Id,
                        Name = request.Name,
                        Success = true
                    };
                }
            }
            catch (Exception ex)
            {
                return new AddProductResponse
                {
                    Name = request.Name,
                    Success = false,
                    Description = ex.Message
                };
            }
        }
    }
}
