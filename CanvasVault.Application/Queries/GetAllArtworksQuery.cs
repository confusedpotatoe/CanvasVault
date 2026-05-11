using CanvasVault.Application.DTOs;
using CanvasVault.Domain.Entities;
using CanvasVault.Domain.Interfaces;
using Mapster;
using MediatR;

namespace CanvasVault.Application.Queries
{
	//	The GetAllArtworksQuery class represents a query that is used to request all artworks from the database.
	//	It implements the IRequest interface from the MediatR library, which allows it to be processed by a corresponding query handler.
	public class GetAllArtworksQuery : IRequest<IEnumerable<ArtworkDto>>
	{
	}

	//	The GetAllArtworksQueryHandler class is responsible for handling the GetAllArtworksQuery and retrieving all artworks from the database using the IArtworkRepository.
	//	It implements the IRequestHandler interface from the MediatR library, which allows it to process the query and return the appropriate result.
	public class GetAllArtworksQueryHandler : IRequestHandler<GetAllArtworksQuery, IEnumerable<Artwork>>
	{
		private readonly IArtworkRepository _artworkRepository;

		//	Constructor that initializes the GetAllArtworksQueryHandler with a reference to the IArtworkRepository.
		public GetAllArtworksQueryHandler(IArtworkRepository artworkRepository)
		{
			_artworkRepository = artworkRepository;
		}

		//	Asynchronous method that handles the GetAllArtworksQuery and retrieves all artworks from the database using the IArtworkRepository.
		public async Task<IEnumerable<Artwork>> Handle(GetAllArtworksQuery request, CancellationToken cancellationToken)
		{
			var artworks = await _artworkRepository.GetAllAsync();

			return artworks.Adapt<IEnumerable<ArtworkDto>>();
		}
	}
}
