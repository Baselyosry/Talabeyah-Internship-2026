using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class OrderProductConfiguration : IEntityTypeConfiguration<OrderProduct>
{
    public void Configure(EntityTypeBuilder<OrderProduct> builder)
    {
        builder.HasKey(op => op.Id);

        builder.Property(op => op.ItemPrice)
            .HasColumnType("decimal(18,2)");

        builder.Property(op => op.TotalPrice)
            .HasColumnType("decimal(18,2)")
            .HasComputedColumnSql("[Quantity] * [ItemPrice]");
    }
}
