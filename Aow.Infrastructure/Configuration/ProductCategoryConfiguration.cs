using AowCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aow.Infrastructure.Configuration
{
    public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.HasMany(c => c.Children)
                .WithOne(c => c.Parent).HasForeignKey(c => c.ParentCategoryId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
