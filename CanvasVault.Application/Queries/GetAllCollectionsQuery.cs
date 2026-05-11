using CanvasVault.Application.DTOs;
using MediatR;

namespace CanvasVault.Application.Queries
{
	public class GetAllCollectionsQuery : IRequest<IEnumerable<CollectionDto>>
	{
	}
}