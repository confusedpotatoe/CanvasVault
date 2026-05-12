using CanvasVault.Application.DTOs;
using CanvasVault.Domain.Interfaces;
using MediatR;


namespace CanvasVault.Application.Commands
{
	public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponseDto>
	{
		private readonly ITokenService _tokenService;

		public LoginCommandHandler(ITokenService tokenService)
		{
			_tokenService = tokenService;
		}

		public async Task<LoginResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
		{
			// For demonstration, we use hardcoded credentials. In a real application, validate against a user store.
			if (request.Username == "admin" && request.Password == "password")
			{
				var roles = new List<string> { "Admin" };
				var token = _tokenService.CreateToken(request.Username, roles);
				return new LoginResponseDto
				{
					Token = token,
					Username = request.Username,
					Roles = roles
				};
			}
			else
			{
				throw new UnauthorizedAccessException("Invalid username or password.");
			}
		}
	}
}
