using CanvasVault.Domain.Entities;
using CanvasVault.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CanvasVault.Infrastructure.Repositories
{
	public class ArtworkRepository : IArtworkRepository
	{

		// The ArtworkRepository class is responsible for managing the data access operations related to the Artwork entity.
		// It interacts with the CanvasVaultDbContext to perform CRUD (Create, Read, Update, Delete) operations on the Artwork table in the database.
		private readonly CanvasVaultDbContext _context;

		// Constructor that initializes the ArtworkRepository with a reference to the CanvasVaultDbContext.
		public ArtworkRepository(CanvasVaultDbContext context)
		{
			_context = context;
		}

		// Asynchronous method to retrieve all artworks from the database.
		public async Task<IEnumerable<Artwork>> GetAllAsync()
		{
			return await _context.Artworks.ToListAsync();
		}

		// Asynchronous method to retrieve a specific artwork by its unique identifier (Id).
		public async Task<Artwork?> GetByIdAsync(int id)
		{
			return await _context.Artworks.FindAsync(id);
		}

		// Asynchronous method to add a new artwork to the database.
		public async Task AddAsync(Artwork artwork)
		{
			await _context.Artworks.AddAsync(artwork);
			await _context.SaveChangesAsync();
		}

		// Asynchronous method to update an existing artwork in the database.
		public async Task UpdateAsync(Artwork artwork)
		{
			_context.Artworks.Update(artwork);
			await _context.SaveChangesAsync();
		}

		// Asynchronous method to delete an artwork from the database by its unique identifier (Id).
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
