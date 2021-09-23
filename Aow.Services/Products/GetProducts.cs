using Aow.Infrastructure.Paging;
using Aow.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aow.Services.Products
{
    [Service]
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
            public Guid? ProductCategoryId { get; set; }
            public Guid? ProductId { get; set; }
            public string Code { get; set; }
            public string ModelNumber { get; set; }
            public string Title { get; set; }
            public string Percent { get; set; }
            public string ProductTaxCode { get; set; }
            public string DiscountType { get; set; } //dynamic amount or %
            public string ItemType { get; set; }
            public string TaxType { get; set; }
            public string ItemTypeId { get; set; }
            public string CategoryName { get; set; }
            public decimal? Value { get; set; }
            public decimal? SalePrice { get; set; }
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
        public IEnumerable<ProductViewModelResponse> Do(PagingParameters pagingParameters, Guid companyId)
        {
            var list = _productRepository.GetProducts(pagingParameters, companyId).GetAwaiter().GetResult();
            var newList = list.Select(x => new ProductViewModelResponse
            {
                Id = x.Id,
                ProductCategoryId = x.ProductCategoryId,
                Name = x.Name,
                Code = x.Code,
                SalePrice = x.SalePrice,
                Value = x.Value,
                Description = x.Description,
                Percent = x.Percent,
                ItemType = x.ItemType
            });

            return newList;
        }
    }
}
