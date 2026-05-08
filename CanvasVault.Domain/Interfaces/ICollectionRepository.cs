namespace CanvasVault.Domain.Interfaces
{
	public interface ICollectionRepository
	{
		Task<IEnumerable<Collection>> GetAllAsync();

		Task<Collection?> GetByIdAsync(int id);
		Task AddAsync(Collection collection);
		Task UpdateAsync(Collection collection);
		Task DeleteAsync(int id);

	}
}
