namespace LAIE.Infrastructure.Authentication;

public interface ITokenService
{
    string GenerateAccessToken(Guid userId, string email, IReadOnlyCollection<string> roles);
    string GenerateRefreshToken();
}
