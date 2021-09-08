using Aow.Infrastructure.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aow.Infrastructure.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(x => x.ProductCategory).WithMany().HasForeignKey(x=>x.ProductCategoryId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
