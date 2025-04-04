using System.Reflection;
using Futures.Application.Data;
using Futures.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Futures.Persistence;

public class FuturesDbContext : DbContext, IFuturesDbContext
{
	public DbSet<FutureContract> FutureContracts { get; set; }
	public DbSet<PricePoint> PricePoints { get; set; }
	public DbSet<PriceDifference> PriceDifferences { get; set; }

	public FuturesDbContext(DbContextOptions<FuturesDbContext> options) : base(options)
	{
		Database.EnsureCreated();
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

		base.OnModelCreating(modelBuilder);
	}
}