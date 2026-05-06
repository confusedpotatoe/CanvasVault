using CanvasVault.Domain.Entities;
using CanvasVault.Domain.Interfaces;
using MediatR;

namespace CanvasVault.Application.Commands
{
	// The CreateArtworkCommand class represents a command that is used to create a new artwork in the database.
	// It implements the IRequest interface from the MediatR library, which allows it to be processed by a corresponding command handler.
	public class CreateArtworkCommand : IRequest<int>
	{
		public string Title { get; set; } = string.Empty;
		public string? Description { get; set; }
		public string ImageUrl { get; set; } = string.Empty;
		public int CollectionId { get; set; }
	}

	// The CreateArtworkCommandHandler class is responsible for handling the CreateArtworkCommand and creating a new artwork in the database using the IArtworkRepository.
	// It implements the IRequestHandler interface from the MediatR library, which allows it to process the command and return the appropriate result (the ID of the newly created artwork).
	public class CreateArtworkCommandHandler : IRequestHandler<CreateArtworkCommand, int>
	{
		private readonly IArtworkRepository _repository;
		public CreateArtworkCommandHandler(IArtworkRepository repository)
		{
			_repository = repository;
		}

		// Asynchronous method that handles the CreateArtworkCommand and creates a new artwork in the database using the IArtworkRepository.
		// It constructs a new Artwork entity based on the properties of the command, adds it to the repository, and returns the ID of the newly created artwork.
		public async Task<int> Handle(CreateArtworkCommand request, CancellationToken cancellationToken)
		{
			var newArtwork = new Artwork
			{
				Title = request.Title,
				Description = request.Description,
				ImageUrl = request.ImageUrl,
				CollectionId = request.CollectionId
			};
			await _repository.AddAsync(newArtwork);
			return newArtwork.Id; // Return the ID of the newly created artwork
		}
	}

}
