using Aow.Infrastructure.IRepositories;
using Aow.Infrastructure.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aow.Services.UserPayment
{
    [Service]
    public class GetUserPayments
    {
        private IRepositoryWrapper _repoWrapper;
        public GetUserPayments(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class UserPaymentsResponse
        {
            public Guid Id { get; set; }
            public string RazorpayPaymentId { get; set; }
            public string RazorpayOrderId { get; set; }
            public string RazorpaySignature { get; set; }
            public string RazorReference { get; set; }
            public string SessionId { get; set; }
            public string UserId { get; set; }
            public Guid CompanyId { get; set; }
            public string Email { get; set; }
            public string ContactNo { get; set; }
            public decimal Amount { get; set; }
            public string upi { get; set; }
            public string rrnNo { get; set; }
            public string status { get; set; }
            public string AddressLine1 { get; set; }
            public string City { get; set; }
            public string PostCode { get; set; }
            public DateTime StartDateUtc { get; set; }
            public DateTime EndDateUtc { get; set; }
            public string Currency { get; set; }
            public string Receipt { get; set; }
            public string Offerid { get; set; }
            public string Attempts { get; set; }
            public string Notes { get; set; }
            public string CreatedAt { get; set; }
            public int NoOfDays { get; set; }
        }

        public async Task<IEnumerable<UserPaymentsResponse>> Do(string userName, Guid cmpId)
        {
            var user = await _repoWrapper.UserRepo.GetUserByName(userName);
            if (user == null)
            {
                return null;
            };
            var list = _repoWrapper.CompanyRepo.GetCompany(cmpId);
            //  var list = _repoWrapper.UserPaymentRepo.GetUserPaymentsByCompany(pagingParameters, user.Id,cmpId).GetAwaiter().GetResult();
            //  var list = _companyRepository.GetCompanies(pagingParameters).GetAwaiter().GetResult();

            var newList = list.UserPayments.Select(x => new UserPaymentsResponse
            {
                Id = x.Id,
                CompanyId = x.CompanyId,
                StartDateUtc = x.StartDateUtc,
                EndDateUtc = x.EndDateUtc,
                NoOfDays = x.NoOfDays
            });

            return newList;
        }
    }
}
