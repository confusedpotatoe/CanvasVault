using CanvasVault.Application.DTOs;
using CanvasVault.Domain.Interfaces;
using Mapster;
using MediatR;

namespace CanvasVault.Application.Queries
{
	public class GetAllArtworksQueryHandler : IRequestHandler<GetAllArtworksQuery, IEnumerable<ArtworkDto>>
	{
		private readonly IArtworkRepository _artworkRepository;

		public GetAllArtworksQueryHandler(IArtworkRepository artworkRepository)
		{
			_artworkRepository = artworkRepository;
		}

		public async Task<IEnumerable<ArtworkDto>> Handle(GetAllArtworksQuery request, CancellationToken cancellationToken)
		{
			var artworks = await _artworkRepository.GetAllAsync();
			return artworks.Adapt<IEnumerable<ArtworkDto>>();
		}
	}
}
