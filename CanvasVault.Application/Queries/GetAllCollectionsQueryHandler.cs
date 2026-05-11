using CanvasVault.Application.DTOs;
using CanvasVault.Domain.Interfaces;
using Mapster;
using MediatR;

namespace CanvasVault.Application.Queries
{
	public class GetAllCollectionsQueryHandler : IRequestHandler<GetAllCollectionsQuery, IEnumerable<CollectionDto>>
	{
		private readonly ICollectionRepository _repository;

		public GetAllCollectionsQueryHandler(ICollectionRepository repository)
		{
			_repository = repository;
		}

		public async Task<IEnumerable<CollectionDto>> Handle(GetAllCollectionsQuery request, CancellationToken cancellationToken)
		{
			var collections = await _repository.GetAllAsync();
			return collections.Adapt<IEnumerable<CollectionDto>>();
		}

	}
}
