using CanvasVault.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CanvasVault.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CollectionsController : Controller
	{
		private readonly IMediator _mediator;

		public CollectionsController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost]
		public async Task<IActionResult> CreateCollection([FromBody] CreateCollectionCommand command)
		{
			var collectionId = await _mediator.Send(command);
			return Ok(new { id = collectionId });
		}

	}
}
