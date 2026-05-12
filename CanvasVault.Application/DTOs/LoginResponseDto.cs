namespace CanvasVault.Application.DTOs
{
	public class LoginResponseDto
	{
		public string Username { get; set; } = string.Empty;
		public string Token { get; set; } = string.Empty;

		public string Password { get; set; } = string.Empty; // This is not recommended in a real application, but included here for demonstration purposes
	}
}
