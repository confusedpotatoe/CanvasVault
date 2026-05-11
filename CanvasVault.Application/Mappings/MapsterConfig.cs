using CanvasVault.Application.DTOs;
using CanvasVault.Domain.Entities;
using Mapster;

public static class MapsterConfig
{
	public static void Configure()
	{
		// configure mapping for Collection to CollectionDetailDto
		TypeAdapterConfig<Collection, CollectionDetailDto>
			.NewConfig()
			.Map(dest => dest.Artworks, src => src.Artworks);
	}
}
