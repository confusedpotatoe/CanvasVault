using CanvasVault.Domain.Interfaces;
using MediatR;

namespace CanvasVault.Application.Commands
{
	public class DeleteArtworkCommand : IRequest<bool>
	{
		public int Id { get; set; }
	}

	public class DeleteArtworkCommandHandler : IRequestHandler<DeleteArtworkCommand, bool>
	{
		private readonly IArtworkRepository _repository;

		public DeleteArtworkCommandHandler(IArtworkRepository repository)
		{
			_repository = repository;
		}

		public async Task<bool> Handle(DeleteArtworkCommand request, CancellationToken cancellationToken)
		{
			var artworkToRemove = await _repository.GetByIdAsync(request.Id);

			if (artworkToRemove == null)
			{
				return false; // Hittades inte
			}

			await _repository.DeleteAsync(artworkToRemove);
			return true; // Borttagning lyckades
		}
	}
}
