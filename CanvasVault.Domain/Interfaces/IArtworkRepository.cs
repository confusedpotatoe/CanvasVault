using CanvasVault.Domain.Entities;

namespace CanvasVault.Domain.Interfaces
{
	public interface IArtworkRepository
	{
		// Asynchronous method to retrieve all artworks from the database
		// Returns a task that represents the asynchronous operation, containing an enumerable collection of Artwork objects.
		Task<IEnumerable<Artwork>> GetAllAsync();

		// Asynchronous method to retrieve a specific artwork by its unique identifier (Id)
		Task<Artwork?> GetByIdAsync(int id);

		// Asynchronous method to add a new artwork to the database
		Task AddAsync(Artwork artwork);

		// Asynchronous method to update an existing artwork in the database
		Task UpdateAsync(Artwork artwork);

		// Asynchronous method to delete an artwork from the database by its unique identifier (Id)
		Task DeleteAsync(int id);
	}

}

