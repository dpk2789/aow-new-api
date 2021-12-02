using Aow.Infrastructure.IRepositories;
using System;
using System.Collections.Generic;

namespace Aow.Services.Manufacture
{
    [Service]
    public class GetManufactureVoucher
    {
        private IRepositoryWrapper _repoWrapper;
        public GetManufactureVoucher(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class GetManufactureVoucherResponse
        {
            public Guid Id { get; set; }
            public DateTime Date { get; set; }
            public string VoucherNumber { get; set; }
            public Guid? FinancialYearId { get; set; }
            public List<ManufactureVariantResponse> Varients { get; set; }
        }
        public class ManufactureVariantResponse
        {
            public Guid Id { get; set; }
            public Guid? VoucherItemVarientId { get; set; }
            public int? SrNo { get; set; }
            public string UniqueNumber { get; set; }
            public string Status { get; set; }
            public string ItemName { get; set; }
            public string Description { get; set; }
            public decimal? SalePrice { get; set; }
            public decimal? MRPPerUnit { get; set; }
            public decimal? Quantity { get; set; }
            public decimal? ConsumedQuantity { get; set; }
            public decimal? ItemAmount { get; set; }
            public decimal Price { get; set; }
            public Guid? ProductVariantId { get; set; }
            public Guid StockId { get; set; }
        }
        public GetManufactureVoucherResponse Do(Guid Id)
        {
            var manufacture = _repoWrapper.ManufacturingRepo.GetManufacture(Id);
            if (manufacture == null)
            {
                return null;
            }
            var vareintList = new List<ManufactureVariantResponse>();
            var getProductViewModel = new GetManufactureVoucherResponse();
            getProductViewModel.Id = manufacture.Id;
            getProductViewModel.Date = manufacture.Date;
            getProductViewModel.VoucherNumber = manufacture.VoucherNumber;
            foreach (var variant in manufacture.ManufactureItems)
            {
                var getProductVarientsResponse = new ManufactureVariantResponse();
                getProductVarientsResponse.Id = variant.Id;
                getProductVarientsResponse.ItemName = variant.Stock.Product.Name;
                getProductVarientsResponse.Quantity = variant.Quantity;
                getProductVarientsResponse.Status = variant.Stock.Status;
                vareintList.Add(getProductVarientsResponse);
            }
            getProductViewModel.Varients = vareintList;
            return getProductViewModel;

        }
    }
}
