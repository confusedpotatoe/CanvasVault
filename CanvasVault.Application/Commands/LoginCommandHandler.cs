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
			var roles = new List<string>();

			if (request.Username.ToLower() == "admin")
			{
				roles.Add("Admin");
				roles.Add("User");
			}
			else
			{
				roles.Add("User");
			}

			// 2. Call the TokenService 
			// Ensure your ITokenService.CreateToken method signature in the Domain layer 
			// accepts (string username, List<string> roles)
			var token = _tokenService.CreateToken(request.Username, roles);

			// 3. Return the DTO
			return new LoginResponseDto
			{
				Username = request.Username,
				Token = token
			};
		}
	}
}
