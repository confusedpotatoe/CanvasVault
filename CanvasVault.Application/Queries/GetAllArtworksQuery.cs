using CanvasVault.Domain.Entities;
using CanvasVault.Domain.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CanvasVault.Application.Queries
{
	//	The GetAllArtworksQuery class represents a query that is used to request all artworks from the database.
	//	It implements the IRequest interface from the MediatR library, which allows it to be processed by a corresponding query handler.
	public class GetAllArtworksQuery : IRequest<IEnumerable<Artwork>>
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
			return await _artworkRepository.GetAllAsync();
		}

		//	Asynchronous method that handles the GetAllArtworksQuery and retrieves all artworks from the database using the IArtworkRepository.
		//	This method is called by the MediatR library when a GetAllArtworksQuery is sent, and it returns an IEnumerable of Artwork objects.
		public async Task<IEnumerable<Artwork>> Handle(GetAllArtworksQuery request, CancellationToken cancellationToken)
		{
			// Handlern anropar repositoryt i Infrastructure-lagret (via interfacet)
			return await _artworkRepository.GetAllAsync();
		}
	}
