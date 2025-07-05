using System;
using System.Threading.Tasks;

namespace DonationTrackingSystem.Domain.Services
{
    public interface ITokenService
    {
        Task<string> GenerateResetTokenAsync(Guid userId, string email);
        Task<bool> ValidateResetTokenAsync(string email, string token);
        Task InvalidateResetTokenAsync(string token); // Opsiyonel, ancak güvenlik için iyi
    }
}
