using Aow.Infrastructure.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aow.Services.ProductVariants
{
    [Service]
    public class GetAllVarientsByCompany
    {
        private IRepositoryWrapper _repoWrapper;
        public GetAllVarientsByCompany(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class GetAllVarientsByCompanyResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public Guid? ProductId { get; set; }
        }
        public IEnumerable<GetAllVarientsByCompanyResponse> Do(Guid companyId)
        {
            var list = _repoWrapper.ProductVarientRepo.GetAllProductVarients(companyId).GetAwaiter().GetResult();
            if (list == null)
            {
                return null;
            }
            var newList = list.Select(x => new GetAllVarientsByCompanyResponse
            {
                Id = x.Id,
                ProductId = x.ProductId,
                Name = x.Name,
            });

            return newList;
        }
    }
}
