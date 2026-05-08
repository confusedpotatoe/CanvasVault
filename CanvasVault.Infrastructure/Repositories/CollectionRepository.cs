using CanvasVault.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CanvasVault.Infrastructure.Repositories
{
	public class CollectionRepository
	{
		private readonly CanvasVaultDbContext _context;

		public CollectionRepository(CanvasVaultDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Collection>> GetAllAsync()
		{
			return await _context.Collections
				.Include(c => c.Artworks)
				.ToListAsync();
		}

		public async Task AddAsync(Collection collection)
		{
			await _context.Collections.AddAsync(collection);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(Collection collection)
		{
			_context.Collections.Update(collection);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var collection = await _context.Collections.FindAsync(id);
			if (collection != null)
			{
				_context.Collections.Remove(collection);
				await _context.SaveChangesAsync();
			}
		}
	}
}
