namespace CanvasVault.Domain.Entities
{
	public class Artwork
	{
		// Primary key for the Artwork entity
		// The Id property is an integer that uniquely identifies each artwork in the database.
		public int Id { get; set; }

		// Title of the artwork
		public string Title { get; set; } = string.Empty;

		// Optional description of the artwork
		public string? Description { get; set; }

		// URL of the artwork's image
		public string ImageUrl { get; set; } = string.Empty;

		// Date and time when the artwork was created
		public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

		// Foreign key to associate the artwork with a collection
		// The CollectionId property is an integer that references the Id of the related Collection entity.
		public int CollectionId { get; set; }

		// Navigation property to represent the relationship between Artwork and Collection
		public Collection? Collection { get; set; }
	}
}
