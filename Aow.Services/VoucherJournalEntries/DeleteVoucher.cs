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
            var voucher = await _repoWrapper.VoucherRepo.GetVoucher(id);
            if (voucher == null)
            {
                return null;
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
    }
}
