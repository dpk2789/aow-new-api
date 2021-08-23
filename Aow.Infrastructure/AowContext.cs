using Aow.Infrastructure.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace Aow.Infrastructure
{
    public class AowContext : DbContext
    {
        public AowContext(DbContextOptions<AowContext> options)
           : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
