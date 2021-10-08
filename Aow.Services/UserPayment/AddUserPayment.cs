using Aow.Infrastructure.IRepositories;
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

        public class OrderRequest
        {
            public string RazorpayPaymentId { get; set; }
            public string RazorpayOrderId { get; set; }
            public string RazorpaySignature { get; set; }
            public string RazorReference { get; set; }
            public string SessionId { get; set; }
            public string UserId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string City { get; set; }
            public string PostCode { get; set; }
        }
        public class AddUserPaymentResponse
        {
            public bool Success { get; set; }
            public string Message { get; set; }
        }
        public async Task<AddUserPaymentResponse> Do(OrderRequest request)
        {
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
                PhoneNumber = request.PhoneNumber,
                Address1 = request.Address1,
                Address2 = request.Address2,
                City = request.City,
                PostCode = request.PostCode,
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
