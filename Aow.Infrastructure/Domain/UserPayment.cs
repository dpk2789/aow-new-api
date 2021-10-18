using System;

namespace Aow.Infrastructure.Domain
{
    public class UserPayment
    {
        public Guid Id { get; set; }
        public string OrderRef { get; set; }
        public string RazorpayPaymentId { get; set; }
        public string RazorpayOrderId { get; set; }
        public string RazorpaySignature { get; set; }        
        public string RazorPayReference { get; set; }
        public string SessionId { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public Guid CompanyId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public decimal Amount { get; set; }
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
        public int NoOfDays { get; set; }
        public bool PaymentStatus { get; set; }
        public bool PrepaidTimeFinished { get; set; }
        public DateTime CreatedUtc {  get; set;}
        public string FirstName { get; set; }
        public string LastName { get; set; }
      
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }       
    }
   
}
