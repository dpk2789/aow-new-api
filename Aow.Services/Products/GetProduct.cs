using Aow.Infrastructure.IRepositories;
using Aow.Infrastructure.Repositories;
using System;
using System.Collections.Generic;

namespace Aow.Services.Products
{
    [Service]
    public class GetProduct
    {
        private IRepositoryWrapper _repoWrapper;
        public GetProduct(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class GetProductViewModel
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
            public Guid ProductCategoryId { get; set; }
            public IEnumerable<ProductImageResponse> ProductImages { get; set; }
        }
        public class ProductImageResponse
        {
            public Guid Id { get; set; }
            public Guid ProductId { get; set; }
            public string Name { get; set; }
            public long Width { get; set; }
            public string RelativePath { get; set; }
            public string GlobalPath { get; set; }
            public string Type { get; set; }
            public string Extention { get; set; }
        }

        public GetProductViewModel Do(Guid Id)
        {
            var product = _repoWrapper.ProductRepo.GetProduct(Id);
            if (product == null)
            {
                return null;
            }
            GetProductViewModel getProductViewModel = new GetProductViewModel
            {
                Id = product.Id,
                ProductCategoryId = product.ProductCategoryId,
                Name = product.Name
            };
            return getProductViewModel;
        }
    }
}
