using Aow.Infrastructure.Common;
using Aow.Infrastructure.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Aow.Infrastructure
{
    public class AowContext : IdentityDbContext<AppUser, IdentityRole, string>
    {
        public AowContext(DbContextOptions<AowContext> options)
           : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Company> Companies { get; set; }


        public override int SaveChanges()
        {
            foreach (var entry in base.ChangeTracker.Entries<IAuditableEntity>())
            {
                IAuditableEntity entity = entry.Entity as IAuditableEntity;

                if (entity != null)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:

                            entry.Entity.CreatedDate = DateTime.UtcNow;
                            break;
                        case EntityState.Modified:

                            entry.Entity.UpdatedDate = DateTime.UtcNow;
                            break;
                    }
                }

            }

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in base.ChangeTracker.Entries<IAuditableEntity>())
            {
                IAuditableEntity entity = entry.Entity as IAuditableEntity;

                if (entity != null)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:

                            entry.Entity.CreatedDate = DateTime.UtcNow;
                            break;
                        case EntityState.Modified:

                            entry.Entity.UpdatedDate = DateTime.UtcNow;
                            break;
                    }
                }

            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
