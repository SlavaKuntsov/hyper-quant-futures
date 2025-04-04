using Futures.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Futures.Persistence.Configurations;

public class PricePointConfiguration : IEntityTypeConfiguration<PricePoint>
{
	public void Configure(EntityTypeBuilder<PricePoint> builder)
	{
		builder.ToTable("PricePoints");

		builder.HasKey(pp => pp.Id);
		
		builder.Property(s => s.Timestamp)
			.IsRequired()
			.HasColumnType("timestamptz")
			.HasConversion(
				v => DateTime.SpecifyKind(v, DateTimeKind.Utc),
				v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
			);

		builder.Property(pp => pp.Price)
			.IsRequired();

		builder.HasOne(pp => pp.FutureContract)
			.WithMany()
			.HasForeignKey(pp => pp.FutureContractId)
			.OnDelete(DeleteBehavior.Cascade);
	}
}