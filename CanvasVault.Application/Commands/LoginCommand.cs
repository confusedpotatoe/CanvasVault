using CanvasVault.Application.DTOs;
using MediatR;

namespace CanvasVault.Application.Commands
{
	public record LoginCommand(string Username, string Password) : IRequest<LoginResponseDto>;
}
