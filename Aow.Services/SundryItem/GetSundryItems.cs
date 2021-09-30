using Aow.Infrastructure.Paging;
using Aow.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aow.Services.SundryItem
{
    [Service]
    public class GetSundryItems
    {
        private IProductRepository _productRepository;
        public GetSundryItems(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public class SundryItemResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }         
            public Guid? ProductCategoryId { get; set; }
            public Guid? ProductId { get; set; }
            public string Code { get; set; }            
            public string Percent { get; set; }          
            public string DiscountType { get; set; } //dynamic amount or %
            public string ItemType { get; set; }
            public string TaxType { get; set; }
            public string ItemTypeId { get; set; }
            public string CategoryName { get; set; }          
        }

        public IEnumerable<SundryItemResponse> Do(PagingParameters pagingParameters, Guid companyId)
        {
            var list = _productRepository.GetProducts(pagingParameters, companyId).GetAwaiter().GetResult();
            var newList = list.Where(x => x.ItemType == "Sundry Item").Select(x => new SundryItemResponse
            {
                Id = x.Id,
                ProductCategoryId = x.ProductCategoryId,
                Name = x.Name,
                Code = x.Code,             
                Percent = x.Percent,
                ItemType = x.ItemType
            });

            return newList;
        }
    }
}
