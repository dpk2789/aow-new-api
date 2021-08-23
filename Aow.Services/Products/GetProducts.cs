using Aow.Infrastructure.Paging;
using Aow.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aow.Services.Products
{
    public class GetProducts
    {
        private IProductRepository _productRepository;
        public GetProducts(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public class ProductViewModelResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
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
        public async Task<IEnumerable<ProductViewModelResponse>> Do(PagingParameters pagingParameters)
        {
            var list = _productRepository.GetProducts(pagingParameters);
            
            return list;
        }
    }
}
