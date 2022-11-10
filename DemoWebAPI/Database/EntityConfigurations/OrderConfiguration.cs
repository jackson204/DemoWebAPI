using DemoWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoWebAPI.Database.EntityConfigurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable(nameof(Order), "dbo")
                .HasComment("訂單主檔")
                .HasKey(c=>c.Id)
                .IsClustered();

            builder.Property(c => c.Id)
                .HasColumnName("Id")
                .HasColumnType("int")
                .IsRequired()
                .HasComment("系統流水號")
                .UseIdentityColumn(1, 1);

            builder.Property(c => c.Name)
                .HasColumnName("Name")
                .HasColumnType("nvarchar(50)")
                .IsRequired()
                .IsUnicode()
                .HasDefaultValueSql("''")
                .HasComment("名稱");

            builder.Property(c => c.Price)
                .HasColumnName("Price")
                .HasColumnType("int")
                .IsRequired()
                .HasComment("價錢");

            builder.Property(c => c.ProductId)
                .HasColumnName("ProductId")
                .HasColumnType("int")
                .IsRequired()
                .HasComment("產品編號");

        }
    }
}
