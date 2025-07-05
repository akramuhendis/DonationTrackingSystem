using System.Security.Claims;

namespace DonationTrackingSystem.Domain.Services
{
    public interface IJwtService
    {
        string GenerateToken(string userId, string email, string role);
        bool ValidateToken(string token, out ClaimsPrincipal? principal);
    }
}