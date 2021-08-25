using Aow.Infrastructure.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Aow.Infrastructure
{
    public class AowContext : IdentityDbContext<AppUser, IdentityRole, string>
    {
        public AowContext(DbContextOptions<AowContext> options)
           : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
