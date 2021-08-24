using Aow.Infrastructure.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Aow.Infrastructure
{
    public class AowContext : IdentityDbContext
    {
        public AowContext(DbContextOptions<AowContext> options)
           : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
