using System;

namespace Aow.Infrastructure.Domain
{
    public class AppUserCompany
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public Company Company { get; set; }
        public string ApplicationUserId { get; set; }
        public AppUser ApplicationUser { get; set; }
    }
}
