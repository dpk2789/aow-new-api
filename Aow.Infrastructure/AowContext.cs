using Aow.Infrastructure.Common;
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
        public DbSet<VoucherItemVariant> VoucherItemVariants { get; set; }
        public DbSet<VoucherSundryItem> VoucherSundryItems { get; set; }
        public DbSet<TransporterDetail> TransporterDetails { get; set; }
        public DbSet<ProductVariantProductAttributeOption> ProductVariantProductAttributeOptions { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<StockProductVariant> StockProductVariants { get; set; }
        public DbSet<Manufacture> Manufactures { get; set; }
        public DbSet<ManufactureItem> ManufactureItems { get; set; }
        public DbSet<ManufacturingVarients> ManufacturingVarients { get; set; }    
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AppUser>(entity => entity.Property(m => m.Id).HasMaxLength(85));
            builder.Entity<AppUser>(entity => entity.Property(m => m.NormalizedEmail).HasMaxLength(85));
            builder.Entity<AppUser>(entity => entity.Property(m => m.NormalizedUserName).HasMaxLength(85));

            builder.Entity<AppUser>(entity => entity.Property(m => m.Id).HasMaxLength(85));
          
            builder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.LoginProvider).HasMaxLength(85));
            builder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.ProviderKey).HasMaxLength(85));
            builder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(85));
            builder.Entity<IdentityUserRole<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(85));

            builder.Entity<IdentityUserRole<string>>(entity => entity.Property(m => m.RoleId).HasMaxLength(85));

            builder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(85));
            builder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.LoginProvider).HasMaxLength(85));
            builder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.Name).HasMaxLength(85));

            builder.Entity<IdentityUserClaim<string>>(entity => entity.Property(m => m.Id).HasMaxLength(85));
            builder.Entity<IdentityUserClaim<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(85));
            builder.Entity<IdentityRoleClaim<string>>(entity => entity.Property(m => m.Id).HasMaxLength(85));
            builder.Entity<IdentityRoleClaim<string>>(entity => entity.Property(m => m.RoleId).HasMaxLength(85));

            builder.Entity<ItemUnit>().Property(p => p.RelationalUnit).HasColumnType("decimal(18,4)");
            builder.Entity<Ledger>().Property(p => p.OpeningBalance).HasColumnType("decimal(18,4)");
            builder.Entity<Product>().Property(p => p.SalePrice).HasColumnType("decimal(18,4)");
            builder.Entity<Product>().Property(p => p.Value).HasColumnType("decimal(18,4)");
            builder.Entity<ProductVariant>().Property(p => p.SalePrice).HasColumnType("decimal(18,4)");
            builder.Entity<JournalEntry>().Property(p => p.CreditAmount).HasColumnType("decimal(18,4)");
            builder.Entity<JournalEntry>().Property(p => p.DebitAmount).HasColumnType("decimal(18,4)");
            builder.Entity<Voucher>().Property(p => p.Total).HasColumnType("decimal(18,4)");
            builder.Entity<VoucherItem>().Property(p => p.DiscountRatePerUnit).HasColumnType("decimal(18,4)");
            builder.Entity<VoucherItem>().Property(p => p.ItemAmount).HasColumnType("decimal(18,4)");
            builder.Entity<VoucherItem>().Property(p => p.MRPPerUnit).HasColumnType("decimal(18,4)");
            builder.Entity<VoucherItem>().Property(p => p.Price).HasColumnType("decimal(18,4)");
            builder.Entity<VoucherItem>().Property(p => p.Quantity).HasColumnType("decimal(18,4)");
            builder.Entity<VoucherSundryItem>().Property(p => p.ItemAmount).HasColumnType("decimal(18,4)");
            builder.Entity<UserPayment>().Property(p => p.Amount).HasColumnType("decimal(18,4)");
            builder.Entity<VoucherItemVariant>().Property(p => p.ItemAmount).HasColumnType("decimal(18,4)");
            builder.Entity<VoucherItemVariant>().Property(p => p.MRPPerUnit).HasColumnType("decimal(18,4)");
            builder.Entity<VoucherItemVariant>().Property(p => p.DiscountRatePerUnit).HasColumnType("decimal(18,4)");
            builder.Entity<VoucherItemVariant>().Property(p => p.UnitQuantity).HasColumnType("decimal(18,4)");
            builder.Entity<Stock>().Property(p => p.ItemAmount).HasColumnType("decimal(18,4)");
            builder.Entity<Stock>().Property(p => p.MRPPerUnit).HasColumnType("decimal(18,4)");
            builder.Entity<Stock>().Property(p => p.Price).HasColumnType("decimal(18,4)");
            builder.Entity<Stock>().Property(p => p.Quantity).HasColumnType("decimal(18,4)");
            builder.Entity<StockProductVariant>().Property(p => p.ItemAmount).HasColumnType("decimal(18,4)");
            builder.Entity<StockProductVariant>().Property(p => p.MRPPerUnit).HasColumnType("decimal(18,4)");
            builder.Entity<StockProductVariant>().Property(p => p.Price).HasColumnType("decimal(18,4)");
            builder.Entity<StockProductVariant>().Property(p => p.Quantity).HasColumnType("decimal(18,4)");
            builder.Entity<StockProductVariant>().Property(p => p.SalePrice).HasColumnType("decimal(18,4)");
            builder.Entity<ManufacturingVarients>().Property(p => p.Quantity).HasColumnType("decimal(18,4)");
            builder.Entity<StockProductVariant>().Property(p => p.ConsumedQuantity).HasColumnType("decimal(18,4)");
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
