using Aow.Infrastructure.IRepositories;
using System;


namespace Aow.Services.ProductAttributeOptions
{
    [Service]
    public class GetProductAttributeOption
    {
        private IRepositoryWrapper _repoWrapper;
        public GetProductAttributeOption(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class GetProductAttributeOptionResponse
        {
            public Guid Id { get; set; }
            public Guid AttributeId { get; set; }
            public string Name { get; set; }
       
        }
        public GetProductAttributeOptionResponse Do(Guid id)
        {
            var category = _repoWrapper.ProductAttributeOptionRepo.GetProductAttributeOption(id);
            var getCompanyResponse = new GetProductAttributeOptionResponse
            {
                Id = category.Id,
                AttributeId = category.ProductAttributeId,
                Name = category.Name,               
            };

            return getCompanyResponse;
        }
    }
}
