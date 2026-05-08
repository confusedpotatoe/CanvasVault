using CanvasVault.Domain.Entities;
using CanvasVault.Domain.Interfaces;
using MediatR;

namespace CanvasVault.Application.Queries
{
	public class GetAllCollectionsQuery : IRequest<IEnumerable<Collection>>
	{
	}

	public class GetAllCollectionsQueryHandler : IRequestHandler<GetAllCollectionsQuery, IEnumerable<Collection>>
	{
		private readonly ICollectionRepository _repository;

		public GetAllCollectionsQueryHandler(ICollectionRepository repository)
		{
			_repository = repository;
		}

		public async Task<IEnumerable<Collection>> Handle(GetAllCollectionsQuery request, CancellationToken cancellationToken)
		{
			return await _repository.GetAllAsync();
		}

	}
}