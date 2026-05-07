using CanvasVault.Domain.Interfaces;
using MediatR;

namespace CanvasVault.Application.Commands
{
	public class UpdateArtworkCommand : IRequest<bool>
	{
		public int Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public string? Description { get; set; }
		public string ImageUrl { get; set; } = string.Empty;
		public int CollectionId { get; set; }

		public class UpdateArtworkCommandHandler : IRequestHandler<UpdateArtworkCommand, bool>
		{
			private readonly IArtworkRepository _repository;

			public UpdateArtworkCommandHandler(IArtworkRepository repository)
			{
				_repository = repository;
			}

			public async Task<bool> Handle(UpdateArtworkCommand request, CancellationToken cancellationToken)
			{
				// get the existing artwork from the repository
				var existingArtwork = await _repository.GetByIdAsync(request.Id);

				if (existingArtwork == null)
				{
					return false; // artwork with the given ID does not exist, so we cannot update it
				}

				// update the values
				existingArtwork.Title = request.Title;
				existingArtwork.Description = request.Description;
				existingArtwork.ImageUrl = request.ImageUrl;
				existingArtwork.CollectionId = request.CollectionId;

				// Save the updated artwork back to the repository
				await _repository.UpdateAsync(existingArtwork);

				return true; // The update was successful!
			}
		}
	}

}