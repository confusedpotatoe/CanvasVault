namespace CanvasVault.Infrastructure
{
	public class CanvasVaultDbContext
	{
		public CanvasVaultDbContext(DbContextOptions<CanvasVaultDbContext> options) : base(options)
		{
		}

		public DbSet<Artwork> Artworks { get; set; }
		public DbSet<Collection> Collections { get; set; }
	}
}
