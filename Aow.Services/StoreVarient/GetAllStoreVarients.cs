using Aow.Infrastructure.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aow.Services.StoreVarient
{
    [Service]
    public class GetAllStoreVarients
    {
        private IRepositoryWrapper _repoWrapper;
        public GetAllStoreVarients(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class GetAllStoreVarientsResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public Guid? ProductId { get; set; }
        }
        public IEnumerable<GetAllStoreVarientsResponse> Do(Guid companyId)
        {
            var list = _repoWrapper.ProductVarientRepo.GetAllProductVarientsByCompany(companyId).GetAwaiter().GetResult();
            if (list == null)
            {
                return null;
            }
            var newList = list.Select(x => new GetAllStoreVarientsResponse
            {
                Id = x.Id,
                ProductId = x.ProductId,
                Name = x.Name,
            });

            return newList;
        }
    }
}
