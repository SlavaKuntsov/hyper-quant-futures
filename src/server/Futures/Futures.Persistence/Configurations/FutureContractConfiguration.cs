using Futures.Domain.Enums;
using Futures.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Futures.Persistence.Configurations;

public class FutureContractConfiguration : IEntityTypeConfiguration<FutureContract>
{
	public void Configure(EntityTypeBuilder<FutureContract> builder)
	{
		builder.ToTable("FutureContracts");

		builder.HasKey(fc => fc.Id);

		builder.Property(fc => fc.Symbol)
			.IsRequired()
			.HasMaxLength(100);
		
		builder.Property(s => s.ExpirationDate)
			.IsRequired()
			.HasColumnType("timestamptz")
			.HasConversion(
				v => DateTime.SpecifyKind(v, DateTimeKind.Utc),
				v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
			);

		builder.Property(fc => fc.Asset)
			.IsRequired()
			.HasMaxLength(50);

		builder.Property(u => u.ContractType)
			.HasConversion(
				role => role.ToString(),
				value => Enum.Parse<ContractType>(value)
			);
	}
}