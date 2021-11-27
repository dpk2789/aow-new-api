using Aow.Infrastructure.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aow.Services.ProductAttributeOptions
{
    [Service]
    public class GetProductAttributeOptionsByProduct
    {
        private IRepositoryWrapper _repoWrapper;
        public GetProductAttributeOptionsByProduct(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class AtrributesByProductResponse
        {
            public Guid? Id { get; set; }
            public string Name { get; set; }
            public string ProductName { get; set; }
            public IEnumerable<AttributesOptionsByProductResponse> AttributesOptionsViewModels { get; set; }
            public bool IsChecked { get; set; }
        }

        public class AttributesOptionsByProductResponse
        {
            public Guid? Id { get; set; }
            public string Name { get; set; }
            public bool IsChecked { get; set; }
        }
        public async Task<IEnumerable<AtrributesByProductResponse>> Do(Guid id)
        {
            var product = _repoWrapper.ProductRepo.GetProduct(id);
            if (product == null)
            {
                return null;
            }
            var attributes = new List<AtrributesByProductResponse>();
            var getAttributes = await _repoWrapper.ProductAttributeRepo.GetProductAttributesByCatrgory(product.ProductCategoryId);
            if (getAttributes.Count == 0)
            {
                return attributes;
            }
            foreach (var attribute in getAttributes)
            {
                AtrributesByProductResponse atrributeOptionsByProductResponse = new AtrributesByProductResponse
                {
                    Id = attribute.Id,
                    Name = attribute.Name,
                    IsChecked = false,
                    ProductName = product.Name,
                    AttributesOptionsViewModels = attribute.ProductAttributeOptions.OrderBy(x => x.Name).Select(x => new AttributesOptionsByProductResponse
                    {
                        Id = x.Id,
                        Name = x.Name,
                        IsChecked = false
                    })
                };
                attributes.Add(atrributeOptionsByProductResponse);
            }

            return attributes;
        }
    }
}
