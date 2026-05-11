namespace CanvasVault.Application.DTOs
{
	public class CollectionDetailDto
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string? Description { get; set; }

		public List<ArtworkDto> Artworks { get; set; } = new();
	}
}
