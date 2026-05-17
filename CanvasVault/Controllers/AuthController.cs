using CanvasVault.Application.Commands;
using CanvasVault.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CanvasVault.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AuthController : ControllerBase
	{
		private readonly IMediator _mediator;

		public AuthController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost("login")]
		public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginCommand command)
		{
			var result = await _mediator.Send(command);
			return Ok(result);
		}
	}
}
