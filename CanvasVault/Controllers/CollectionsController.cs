using CanvasVault.Application.Commands;
using CanvasVault.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CanvasVault.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CollectionsController : ControllerBase
	{
		private readonly IMediator _mediator;

		public CollectionsController(IMediator mediator)
		{
			_mediator = mediator;
		}
		[HttpGet]
		public async Task<IActionResult> GetCollections()
		{
			// Implementation for fetching collections
			var collections = await _mediator.Send(new GetAllCollectionsQuery());
			return Ok(collections);
		}

		[HttpPost]
		public async Task<IActionResult> CreateCollection([FromBody] CreateCollectionCommand command)
		{
			var collectionId = await _mediator.Send(command);
			return CreatedAtAction(nameof(GetCollections), new { id = collectionId }, new { id = collectionId });
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateCollection(int id, [FromBody] UpdateCollectionCommand command)
		{
			if (id != command.Id)
			{
				return BadRequest("ID in URL does not match ID in body.");
			}
			await _mediator.Send(command);
			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCollection(int id)
		{
			var command = new DeleteCollectionCommand { Id = id };
			await _mediator.Send(command);
			return NoContent();
		}



	}
}
