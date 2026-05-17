using CanvasVault.Application.Commands;
using CanvasVault.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CanvasVault.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Authorize] // This attribute ensures that all endpoints in this controller require authentication

	public class ArtworksController : ControllerBase
	{
		private readonly IMediator _mediator;

		// Constructor that initializes the ArtworksController with a reference to the IMediator instance.
		// The IMediator instance is used to send commands and queries to the MediatR pipeline, which allows for a clean separation of concerns between the controller and the application logic.
		public ArtworksController(IMediator mediator)
		{
			_mediator = mediator;
		}

		// Asynchronous method that handles HTTP GET requests to retrieve all artworks from the database.
		// The method sends a GetAllArtworksQuery to the MediatR pipeline, which is handled by a corresponding query handler that retrieves the artworks from the database and returns them as a response.
		[HttpGet]
		public async Task<IActionResult> GetAllArtworks()
		{
			var query = new GetAllArtworksQuery();
			var artworks = await _mediator.Send(query);
			return Ok(artworks);
		}

		// Asynchronous method that handles HTTP POST requests to create a new artwork in the database.
		// The method takes a CreateArtworkCommand object as input, which contains the necessary information to create a new artwork.
		[HttpPost]
		public async Task<IActionResult> CreateArtwork([FromBody] CreateArtworkCommand command)
		{
			var newArtworkId = await _mediator.Send(command);
			return Ok(new { Message = "Artwork created successfully", ArtworkId = newArtworkId });
		}

		// Asynchronous method that handles HTTP PUT requests to update an existing artwork in the database.
		// The method takes an ID parameter from the URL and an UpdateArtworkCommand object from the request body, which contains the updated information for the artwork.
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateArtwork(int id, [FromBody] UpdateArtworkCommand command)
		{
			if (id != command.Id)
			{
				return BadRequest("ID in URL does not match ID in request body.");
			}
			var result = await _mediator.Send(command);
			if (!result)
			{
				return NotFound("Artwork with the specified ID was not found.");
			}
			return Ok(new { Message = "Artwork updated successfully" });
		}

		// Asynchronous method that handles HTTP DELETE requests to delete an existing artwork from the database.
		// The method takes an ID parameter from the URL, which is used to identify the artwork to be deleted.
		[HttpDelete("{id}")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> DeleteArtwork(int id)
		{
			var command = new DeleteArtworkCommand { Id = id };
			var result = await _mediator.Send(command);

			if (!result)
			{
				return NotFound($"Kunde inte hitta ett konstverk med ID {id} att ta bort.");
			}

			return NoContent(); // 204 No Content är standard när man framgångsrikt tagit bort något
		}
	}
}
