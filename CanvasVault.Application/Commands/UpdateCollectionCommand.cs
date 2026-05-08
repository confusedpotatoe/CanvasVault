using CanvasVault.Domain.Interfaces;
using MediatR;

namespace CanvasVault.Application.Commands
{
	public class UpdateCollectionCommand : IRequest<Unit>
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string? Description { get; set; }
	}

	public class UpdateCollectionCommandHandler : IRequestHandler<UpdateCollectionCommand, Unit>
	{
		private readonly ICollectionRepository _repository;
		public UpdateCollectionCommandHandler(ICollectionRepository repository) => _repository = repository;

		public async Task<Unit> Handle(UpdateCollectionCommand request, CancellationToken cancellationToken)
		{
			var collection = await _repository.GetByIdAsync(request.Id);
			if (collection == null) return Unit.Value;

			collection.Name = request.Name;
			collection.Description = request.Description;

			await _repository.UpdateAsync(collection);
			return Unit.Value;
		}
	}
}
