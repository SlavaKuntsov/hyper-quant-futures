using Futures.Domain.Enums;
using Futures.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Futures.Persistence.Configurations;

public class PriceDifferenceConfiguration : IEntityTypeConfiguration<PriceDifference>
{
	public void Configure(EntityTypeBuilder<PriceDifference> builder)
	{
		builder.ToTable("PriceDifferences");

		builder.HasKey(pd => pd.Id);
		
		builder.Property(s => s.Timestamp)
			.IsRequired()
			.HasColumnType("timestamptz")
			.HasConversion(
				v => DateTime.SpecifyKind(v, DateTimeKind.Utc),
				v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
			);

		builder.Property(u => u.IntervalType)
			.HasConversion(
				role => role.ToString(),
				value => Enum.Parse<TimeIntervalType>(value)
			);

		builder.Property(pd => pd.Difference)
			.IsRequired();

		builder.HasOne(pd => pd.CurrentFutureContract)
			.WithMany()
			.HasForeignKey(pd => pd.CurrentFuturesContractId)
			.OnDelete(DeleteBehavior.Restrict);

		builder.HasOne(pd => pd.NextFutureContract)
			.WithMany()
			.HasForeignKey(pd => pd.NextFuturesContractId)
			.OnDelete(DeleteBehavior.Restrict);
	}
}