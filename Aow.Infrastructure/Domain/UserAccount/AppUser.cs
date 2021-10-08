using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Aow.Infrastructure.Domain
{
    public class AppUser : IdentityUser
    {
        public virtual IList<AppUserCompany> AppUserCompanies { get; set; }
        public virtual IList<UserPayment> UserPayments { get; set; }
    }
}
