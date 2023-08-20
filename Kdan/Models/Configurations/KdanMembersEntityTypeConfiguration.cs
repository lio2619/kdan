using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kdan.Models.Configurations
{
    public class KdanMembersEntityTypeConfiguration : IEntityTypeConfiguration<KdanMembers>
    {
        public void Configure(EntityTypeBuilder<KdanMembers> builder)
        {
            builder
                .ToTable("members", c => c.HasComment("員工打卡時間"))
                .HasKey(c => c.Id);

            builder
                .Property(c => c.Id)
                .HasMaxLength(36)
                .HasComment("流水編號");

            builder
                .Property(c => c.EmployeeNumber)
                .IsRequired()
                .HasMaxLength(36)
                .HasComment("員工編號");

            builder
                .Property(c => c.ClockIn)
                .HasComment("上班打卡時間");

            builder
                .Property(c => c.ClockOut)
                .HasComment("下班打卡時間");
        }
    }
}
