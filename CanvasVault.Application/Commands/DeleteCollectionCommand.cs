using CanvasVault.Domain.Interfaces;
using MediatR;

namespace CanvasVault.Application.Commands
{
	public class DeleteCollectionCommand : IRequest<Unit>
	{
		public int Id { get; set; }
	}

	public class DeleteCollectionCommandHandler : IRequestHandler<DeleteCollectionCommand, Unit>
	{
		private readonly ICollectionRepository _repository;
		public DeleteCollectionCommandHandler(ICollectionRepository repository)
		{
			_repository = repository;
		}
		public async Task<Unit> Handle(DeleteCollectionCommand request, CancellationToken cancellationToken)
		{
			await _repository.DeleteAsync(request.Id);
			return Unit.Value;
		}
	}

}