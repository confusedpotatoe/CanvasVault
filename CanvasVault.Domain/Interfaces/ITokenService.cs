namespace CanvasVault.Domain.Interfaces
{
	public interface ITokenService
	{
		string CreateToken(string username, List<string> roles);
	}
}
