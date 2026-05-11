using CanvasVault.Application.DTOs;
using MediatR;

namespace CanvasVault.Application.Queries
{
	//	The GetAllArtworksQuery class represents a query that is used to request all artworks from the database.
	//	It implements the IRequest interface from the MediatR library, which allows it to be processed by a corresponding query handler.
	public class GetAllArtworksQuery : IRequest<IEnumerable<ArtworkDto>>
	{
	}
}

