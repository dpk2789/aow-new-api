using Aow.Infrastructure.IRepositories;
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
            public IEnumerable<GetProductVarientsResponse> ProductVariants { get; set; }
        }
        public class GetProductVarientsResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string SalePrice { get; set; }
        }

        public GetProductViewModel Do(Guid Id)
        {
            var product = _repoWrapper.ProductRepo.GetProduct(Id);
            if (product == null)
            {
                return null;
            }
            var vareintList = new List<GetProductVarientsResponse>();
            var getProductViewModel = new GetProductViewModel();
            getProductViewModel.Id = product.Id;
            getProductViewModel.ProductCategoryId = product.ProductCategoryId;
            getProductViewModel.Name = product.Name;
            foreach (var variant in product.ProductVariants)
            {
                var getProductVarientsResponse = new GetProductVarientsResponse();
                getProductVarientsResponse.Id = variant.Id;
                getProductVarientsResponse.Name = variant.Name;
                vareintList.Add(getProductVarientsResponse);
            }
            getProductViewModel.ProductVariants = vareintList;
            return getProductViewModel;
        }
    }
}
