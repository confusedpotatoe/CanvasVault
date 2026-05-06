using CanvasVault.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CanvasVault.Infrastructure
{
	public class CanvasVaultDbContext : DbContext
	{
		public CanvasVaultDbContext(DbContextOptions<CanvasVaultDbContext> options) : base(options)
		{
		}

		public DbSet<Artwork> Artworks { get; set; }
		public DbSet<Collection> Collections { get; set; }
	}
}
