using Aow.Infrastructure.IRepositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aow.Services.VoucherInvoice
{
    [Service]
    public class GetAllVoucherVarients
    {
        private IRepositoryWrapper _repoWrapper;
        public GetAllVoucherVarients(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class VoucherAllVarientResponse
        {
            public Guid? Id { get; set; }
            public string VoucherName { get; set; }
            public string VoucherNumber { get; set; }
            public DateTime? Date { get; set; }
            public decimal? ItemsTotal { get; set; }
            public virtual List<GetAllVoucherVarientsResponse> VoucherItemVarients { get; set; }
        }
        public class GetAllVoucherVarientsResponse
        {
            public Guid Id { get; set; }
            public Guid? ItemId { get; set; }
            public int? SrNo { get; set; }
            public decimal? DiscountRatePerUnit { get; set; }
            public decimal? MRPPerUnit { get; set; }
            public decimal? Rate { get; set; }
            public decimal? Quantity { get; set; }
            public decimal? ItemAmount { get; set; }
            public string ItemName { get; set; }
            public string VarientName { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
            public Guid ProductId { get; set; }
        }

        public async Task<VoucherAllVarientResponse> Do(Guid id)
        {
            var voucher = await _repoWrapper.VoucherRepo.GetVoucherVairents(id);
            var varientList = new List<GetAllVoucherVarientsResponse>();
            VoucherAllVarientResponse voucherAllVarientResponse = new VoucherAllVarientResponse();
            voucherAllVarientResponse.Id = voucher.Id;
            voucherAllVarientResponse.VoucherName = voucher.VoucherName;
            voucherAllVarientResponse.VoucherNumber = voucher.VoucherNumber;
            foreach (var item in voucher.VoucherItems)
            {
                foreach (var varient in item.VoucherItemVariants)
                {
                    GetAllVoucherVarientsResponse getAllVoucherVarientsResponse = new GetAllVoucherVarientsResponse
                    {
                        Id = varient.Id,
                        ItemId = item.Id,
                        VarientName = varient.ProductVariant.Name,
                        ItemName = varient.ProductVariant.Products.Name,
                        Rate = item.MRPPerUnit,
                        Quantity = varient.UnitQuantity,
                        ItemAmount = varient.ItemAmount,
                        Description = item.Description
                    };
                    varientList.Add(getAllVoucherVarientsResponse);
                }
            }
            voucherAllVarientResponse.VoucherItemVarients = varientList;
            return voucherAllVarientResponse;
        }
    }
}
