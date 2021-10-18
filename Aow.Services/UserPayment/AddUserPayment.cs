using Aow.Infrastructure.IRepositories;
using System;
using System.Threading.Tasks;

namespace Aow.Services.UserPayment
{
    [Service]
    public class AddUserPayment
    {
        private IRepositoryWrapper _repoWrapper;
        public AddUserPayment(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }

        public class CreateResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public bool Success { get; set; }
        }
        public class AddPaymentRequest
        {
            public string RazorpayPaymentId { get; set; }
            public string RazorpayOrderId { get; set; }
            public string RazorpaySignature { get; set; }
            public string RazorReference { get; set; }
            public string SessionId { get; set; }
            public string UserId { get; set; }
            public Guid CompanyId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string ContactNo { get; set; }
            public decimal Amount { get; set; }
            public int? NoOfDays { get; set; }
            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string City { get; set; }
            public string PostCode { get; set; }
            public string upi { get; set; }
            public string rrnNo { get; set; }
            public string status { get; set; }
            public DateTime StartDateUtc { get; set; }
            public DateTime EndDateUtc { get; set; }
            public string CreatedAt { get; set; }
            public string Currency { get; set; }
            public string Receipt { get; set; }
            public string Offerid { get; set; }
            public string Attempts { get; set; }
            public string Notes { get; set; }

        }
        public class AddUserPaymentResponse
        {
            public bool Success { get; set; }
            public string Message { get; set; }
        }
        public async Task<AddUserPaymentResponse> Do(AddPaymentRequest request)
        {
            var user = await _repoWrapper.UserRepo.GetUserByName(request.UserId);
            if (user == null)
            {
                return null;
            };

            var orderByUser = new Aow.Infrastructure.Domain.UserPayment
            {
                OrderRef = request.RazorReference,
                RazorPayReference = request.RazorReference,
                RazorpayOrderId = request.RazorpayOrderId,
                RazorpayPaymentId = request.RazorpayPaymentId,
                RazorpaySignature = request.RazorpaySignature,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.ContactNo,
                Address1 = request.Address1,
                Address2 = request.Address2,
                City = request.City,
                PostCode = request.PostCode,               
                rrnNo = request.rrnNo,              
                upi = request.upi,
                Amount = request.Amount,
                StartDateUtc = request.StartDateUtc,
                EndDateUtc = request.EndDateUtc,
                NoOfDays = request.NoOfDays.Value,
                CompanyId = request.CompanyId,
                AppUserId = user.Id
            };

            _repoWrapper.UserPaymentRepo.Create(orderByUser);
            AddUserPaymentResponse addUserPaymentResponse = new AddUserPaymentResponse();
            int i = await _repoWrapper.SaveNew();
            if (i > 0)
            {
                addUserPaymentResponse.Success = true;
                return addUserPaymentResponse;
            }
            addUserPaymentResponse.Success = false;
            return addUserPaymentResponse;
        }

    }
}
