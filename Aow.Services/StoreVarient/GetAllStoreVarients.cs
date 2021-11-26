﻿using Aow.Infrastructure.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;

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
            public decimal Quantity { get; set; }
            public Guid? ProductVarientId { get; set; }
        }
        public IEnumerable<GetAllStoreVarientsResponse> Do(Guid companyId)
        {
            var list = _repoWrapper.StockVarientRepo.GetStockVarientByCompany(companyId).GetAwaiter().GetResult();
            if (list == null)
            {
                return null;
            }
            var newList = list.Select(x => new GetAllStoreVarientsResponse
            {
                Id = x.Id,
                ProductVarientId = x.ProductVariant.Id,
                Name = x.ProductVariant.Name,
                Quantity = x.Quantity.Value
            });

            return newList;
        }
    }
}
