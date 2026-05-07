using CanvasVault.Application.Commands;
using CanvasVault.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CanvasVault.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ArtworksController : ControllerBase
	{
		private readonly IMediator _mediator;

		public ArtworksController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllArtworks()
		{
			var query = new GetAllArtworksQuery();
			var artworks = await _mediator.Send(query);
			return Ok(artworks);
		}

		[HttpPost]
		public async Task<IActionResult> CreateArtwork([FromBody] CreateArtworkCommand command)
		{
			var newArtworkId = await _mediator.Send(command);
			return Ok(new { Message = "Artwork created successfully", ArtworkId = newArtworkId });
		}
	}
}
