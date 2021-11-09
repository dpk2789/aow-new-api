using Aow.Infrastructure.IRepositories;
using System;

namespace Aow.Services.VoucherItemVarient
{
    public class GetVoucherItemVarient
    {
        private IRepositoryWrapper _repoWrapper;
        public GetVoucherItemVarient(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }

        public class GetVoucherItemVarientResponse
        {
            public Guid Id { get; set; }
            public int? SrNo { get; set; }
            public decimal? DiscountRatePerUnit { get; set; }
            public decimal? MRPPerUnit { get; set; }
            public decimal? Quantity { get; set; }
            public decimal? ItemAmount { get; set; }
            public string ItemName { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
            public Guid ProductId { get; set; }
        }
      

        public GetVoucherItemVarientResponse Do(Guid id)
        {
            var voucherItemVarient = _repoWrapper.VoucherItemVarientRepo.GetVoucherItemVarient(id);
            GetVoucherItemVarientResponse getVoucherItemResponse = new GetVoucherItemVarientResponse();
            getVoucherItemResponse.Id = voucherItemVarient.Id;
            return getVoucherItemResponse;
        }
    }
}
