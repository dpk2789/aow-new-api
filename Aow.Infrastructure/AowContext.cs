using Aow.Infrastructure.Common;
using Aow.Infrastructure.Configuration;
using Aow.Infrastructure.Domain;
using AowCore.Domain;
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

       
        public DbSet<Company> Companies { get; set; }
        public DbSet<AppUserCompany> AppUserCompanies { get; set; }
        public DbSet<FinancialYear> FinancialYears { get; set; }

        public DbSet<Ledger> Ledgers { get; set; }
        public DbSet<LedgerCategory> LedgerCategories { get; set; }

        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ItemUnit> ItemUnits { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductGroup> ProductGroups { get; set; }
        public DbSet<ItemGroupProduct> ItemGroupProducts { get; set; }
        public DbSet<ProductAttribute> ProductAttributes { get; set; }
        public DbSet<ProductAttributeOptions> ProductAttributeOptions { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }

        public DbSet<JournalEntry> JournalEntries { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<VoucherItem> VoucherItems { get; set; }        
        public DbSet<VoucherSundryItem> VoucherSundryItems { get; set; }
        public DbSet<TransporterDetail> TransporterDetails { get; set; }
        public DbSet<ProductVariantProductAttributeOption> ProductVariantProductAttributeOptions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ItemUnit>().Property(p => p.RelationalUnit).HasColumnType("decimal(18,4)");
            builder.Entity<Ledger>().Property(p => p.OpeningBalance).HasColumnType("decimal(18,4)");
            builder.Entity<ProductVariant>().Property(p => p.SalePrice).HasColumnType("decimal(18,4)");
           // builder.ApplyConfiguration(new ProductCategoryConfiguration());
            base.OnModelCreating(builder);
        }

       
        public override  Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
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

            return  base.SaveChangesAsync(cancellationToken);
        }
    }
}
