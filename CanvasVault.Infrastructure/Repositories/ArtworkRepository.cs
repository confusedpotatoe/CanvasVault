namespace CanvasVault.Infrastructure.Repositories
{
	public class ArtworkRepository
	{
		private readonly CanvasVaultDbContext _context;

		public ArtworkRepository(CanvasVaultDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Artwork>> GetAllAsync()
		{
			return await _context.Artworks.ToListAsync();
		}

		public async Task<Artwork> GetByIdAsync(int id)
		{
			return await _context.Artworks.FindAsync(id);
		}

		public async Task AddAsync(Artwork artwork)
		{
			await _context.Artworks.AddAsync(artwork);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(Artwork artwork)
		{
			_context.Artworks.Update(artwork);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var artwork = await _context.Artworks.FindAsync(id);
			if (artwork != null)
			{
				_context.Artworks.Remove(artwork);
				await _context.SaveChangesAsync();
			}
		}
	}
}
