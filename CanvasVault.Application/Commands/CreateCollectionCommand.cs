using CanvasVault.Domain.Entities;
using CanvasVault.Domain.Interfaces;
using MediatR;

namespace CanvasVault.Application.Commands
{
	public class CreateCollectionCommand
	{
		public class CreateCollectionCommand : IRequest<int>
		{
			public string Name { get; set; } = string.Empty;
			public string? Description { get; set; }
		}

		public class CreateCollectionCommandHandler : IRequestHandler<CreateCollectionCommand, int>
		{
			private readonly ICollectionRepository _repository;
			public CreateCollectionCommandHandler(ICollectionRepository repository)
			{
				_repository = repository;
			}
			public async Task<int> Handle(CreateCollectionCommand request, CancellationToken cancellationToken)
			{
				var newCollection = new Collection
				{
					Name = request.Name,
					Description = request.Description
				};
				await _repository.AddAsync(newCollection);
				return newCollection.Id; // Return the ID of the newly created collection
			}
		}
	}
}
