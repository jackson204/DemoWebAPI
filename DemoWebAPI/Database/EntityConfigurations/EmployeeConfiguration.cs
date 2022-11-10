using DemoWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoWebAPI.Database.EntityConfigurations
{
    public sealed class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable(nameof(Employee), "dbo")
                .HasComment("員工主檔")
                .HasKey(c => c.Id)
                .IsClustered();

            builder.Property(c => c.Id)
                .HasColumnName("Id")
                .HasColumnType("int")
                .IsRequired()
                .HasComment("系統流水號")
                .UseIdentityColumn(1, 1);

            builder.Property(c => c.Name)
                .HasColumnName("Name")
                .HasColumnType("varchar(30)")
                .IsRequired()
                .IsUnicode(false)
                .HasComment("姓名(帳號)");

            builder.Property(c => c.Age)
                .HasColumnName("Age")
                .HasColumnType("tinyint")
                .IsRequired(false)
                .HasComment("年紀");

            builder.Property(c => c.Password)
                .HasColumnName("Password")
                .HasColumnType("varchar(60)")
                .IsRequired()
                .IsUnicode(false)
                .HasComment("密碼");

            builder.Property(c => c.Role)
                .HasColumnName("Role")
                .HasColumnType("nvarchar(30)")
                .IsRequired()
                .HasDefaultValueSql("''")
                .IsUnicode()
                .HasComment("角色");

            builder.Property(c => c.CreatedDate)
                .HasColumnName("CreatedDate")
                .HasColumnType("datetime2")
                .IsRequired()
                .HasDefaultValueSql("getdate()")
                .IsUnicode()
                .HasComment("建立日期");

        }
    }
}
