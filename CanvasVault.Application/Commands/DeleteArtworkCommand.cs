using CanvasVault.Domain.Interfaces;
using MediatR;

namespace CanvasVault.Application.Commands
{
	// 1. Själva kommandot: Innehåller bara ID:t på det konstverk vi vill ta bort
	public class DeleteArtworkCommand : IRequest<bool>
	{
		public int Id { get; set; }
	}

	// 2. Handlern: Tar emot kommandot och sköter logiken
	public class DeleteArtworkCommandHandler : IRequestHandler<DeleteArtworkCommand, bool>
	{
		private readonly IArtworkRepository _repository;

		// Injicera interfacet (från Domain-lagret)
		public DeleteArtworkCommandHandler(IArtworkRepository repository)
		{
			_repository = repository;
		}

		public async Task<bool> Handle(DeleteArtworkCommand request, CancellationToken cancellationToken)
		{
			// Steg 1: Leta upp konstverket i databasen med det inskickade ID:t. 
			// Vi använder 'var' eftersom metoden returnerar ett Artwork.
			var artworkToRemove = await _repository.GetByIdAsync(request.Id);

			// Steg 2: Kontrollera om konstverket faktiskt finns
			if (artworkToRemove == null)
			{
				return false; // Hittades inte, returnera false
			}

			// Steg 3: Om det hittades, anropa DeleteAsync i ditt repository.
			// Här skickar vi in request.Id eftersom ditt IArtworkRepository kräver en int.
			await _repository.DeleteAsync(request.Id);

			// Return true för att indikera att borttagningen lyckades
			return true;
		}
	}
}