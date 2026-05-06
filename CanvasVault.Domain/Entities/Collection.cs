namespace CanvasVault.Domain.Entities
{
	public class Collection
	{
		// Primary key for the Collection entity
		// The Id property is an integer that uniquely identifies each collection in the database.
		public int Id { get; set; }

		// Name of the collection
		public string Name { get; set; } = string.Empty;

		// Optional description of the collection
		public string? Description { get; set; }

		// Navigation property to represent the relationship between Collection and Artwork
		// A collection can contain multiple artworks, so we use a list to hold the related artworks.
		public List<Artwork> Artworks { get; set; } = new List<Artwork>();
	}
}
