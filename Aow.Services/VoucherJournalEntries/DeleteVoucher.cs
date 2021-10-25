using Aow.Infrastructure.IRepositories;
using System;
using System.Threading.Tasks;

namespace Aow.Services.Voucher
{
    [Service]
    public class DeleteVoucher
    {
        private IRepositoryWrapper _repoWrapper;
        public DeleteVoucher(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class DeleteVoucherResponse
        {
            public string Message { get; set; }
            public bool Success { get; set; }
        }

        public async Task<DeleteVoucherResponse> Do(Guid id)
        {
            try
            {
                var voucher = await _repoWrapper.VoucherRepo.GetVoucherForDelete(id);
                if (voucher == null)
                {
                    return null;
                }
                if (voucher.JournalEntries != null)
                {
                    foreach (var jentry in voucher.JournalEntries)
                    {
                        _repoWrapper.JournalEntryRepo.Delete(jentry);
                    }
                }
                if (voucher.VoucherSundryItems != null)
                {
                    foreach (var sundryItem in voucher.VoucherSundryItems)
                    {
                        _repoWrapper.VoucherSundryItemRepo.Delete(sundryItem);
                    }
                }
                if (voucher.VoucherItems != null)
                {
                    foreach (var item in voucher.VoucherItems)
                    {
                        _repoWrapper.VoucherItemRepo.Delete(item);
                    }
                }
                _repoWrapper.VoucherRepo.Delete(voucher);
                int i = await _repoWrapper.SaveNew();
                if (i <= 0)
                {
                    return new DeleteVoucherResponse
                    {
                        Message = "Error Deleting",
                        Success = false
                    };
                }
                else
                {
                    return new DeleteVoucherResponse
                    {
                        Message = "Voucher Deleted",
                        Success = true
                    };
                }
            }
            catch (Exception ex)
            {
                return new DeleteVoucherResponse
                {
                    Message = ex.Message,
                    Success = false
                };
            }
        }
    }
}
