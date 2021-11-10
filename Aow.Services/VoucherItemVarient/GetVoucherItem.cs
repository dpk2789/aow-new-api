using Aow.Infrastructure.IRepositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aow.Services.VoucherItemVarient
{
    [Service]
    public class GetVoucherItem
    {
        private IRepositoryWrapper _repoWrapper;
        public GetVoucherItem(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }

        public class GetVoucherItemResponse
        {
            public Guid Id { get; set; }          
            public decimal ItemsTotal { get; set; }
            public string ItemName { get; set; }         
            public Guid ProductId { get; set; }
            public virtual List<GetVoucherItemVarientResponse> Varients { get; set; }
        }

        public class GetVoucherItemVarientResponse
        {
            public Guid Id { get; set; }
            public Guid VarientId { get; set; }
            public Guid VoucherItemId { get; set; }
            public int? SrNo { get; set; }
            public decimal? DiscountRatePerUnit { get; set; }
            public decimal? MRPPerUnit { get; set; }
            public decimal? Quantity { get; set; }
            public decimal? ItemAmount { get; set; }
            public string ItemName { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
        }

        public async Task<GetVoucherItemResponse> Do(Guid id)
        {
            var voucherItem = await _repoWrapper.VoucherItemRepo.GetVoucherItem(id);
            if (voucherItem == null)
            {
                return null;
            }
            var voucherViewModel = new GetVoucherItemResponse
            {
                Id = voucherItem.Id,
                ItemName = voucherItem.Product.Name,
                ProductId = voucherItem.Product.Id,
            };
            decimal ItemsTotal = 0;
            var varients = new List<GetVoucherItemVarientResponse>();
            if (voucherItem.VoucherItemVariants != null)
            {
                foreach (var varient in voucherItem.VoucherItemVariants)
                {
                    var viewModel = new GetVoucherItemVarientResponse();
                    viewModel.Id = varient.Id;
                    viewModel.SrNo = varient.SrNo;
                    viewModel.ItemName = varient.Name;
                    viewModel.Quantity = varient.UnitQuantity;
                    viewModel.ItemAmount = varient.ItemAmount;
                    viewModel.MRPPerUnit = varient.MRPPerUnit;
                    viewModel.SrNo = varient.SrNo;
                    viewModel.VarientId = varient.ProductVariantId.Value;                  
                    varients.Add(viewModel);
                    ItemsTotal = varient.ItemAmount.Value + ItemsTotal;
                }
                voucherViewModel.Varients = varients;
            }
            voucherViewModel.ItemsTotal = ItemsTotal;
            return voucherViewModel;
        }
    }
}
