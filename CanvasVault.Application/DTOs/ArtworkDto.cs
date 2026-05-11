namespace CanvasVault.Application.DTOs
{
	public class ArtworkDto
	{
		public int Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public string Artist { get; set; } = string.Empty;
		public int Year { get; set; }
		public int CollectionId { get; set; }

	}
}
