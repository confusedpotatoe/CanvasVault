using CanvasVault.Domain.Interfaces;
using MediatR;

namespace CanvasVault.Application.Commands
{
	// 1. The Command
	public class DeleteArtworkCommand : IRequest<bool>
	{
		public int Id { get; set; }
	}

	// 2. The Handler
	public class DeleteArtworkCommandHandler : IRequestHandler<DeleteArtworkCommand, bool>
	{
		private readonly IArtworkRepository _repository;

		public DeleteArtworkCommandHandler(IArtworkRepository repository)
		{
			_repository = repository;
		}

		public async Task<bool> Handle(DeleteArtworkCommand request, CancellationToken cancellationToken)
		{
			// Use 'var' here because GetByIdAsync returns an Artwork, not a bool
			var artworkToRemove = await _repository.GetByIdAsync(request.Id);

			// Check if the object is 'null', not 'false'
			if (artworkToRemove == null)
			{
				// We return a bool (false) here to tell the controller it failed
				return false;
			}

			// Call the repository with the ID, perfectly matching your interface
			await _repository.DeleteAsync(request.Id);

			// We return a bool (true) here to tell the controller it succeeded
			return true;
		}
	}
}