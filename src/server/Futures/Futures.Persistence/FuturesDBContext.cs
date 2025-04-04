using System.Reflection;
using Futures.Application.Data;
using Microsoft.EntityFrameworkCore;

namespace Futures.Persistence;

public class FuturesDbContext : DbContext, IFuturesDbContext
{
	// public DbSet<UserEntity> Users { get; set; }

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