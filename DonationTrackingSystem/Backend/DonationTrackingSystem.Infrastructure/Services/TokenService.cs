using DonationTrackingSystem.Domain.Services;
using System;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace DonationTrackingSystem.Infrastructure.Services
{
    // DİKKAT: Bu, sadece bir yer tutucu uygulamadır ve üretim ortamında KULLANILMAMALIDIR.
    // Gerçek bir uygulamada, token'lar veritabanında güvenli bir şekilde saklanmalı,
    // süreleri yönetilmeli ve tek kullanımlık olmaları sağlanmalıdır.
    public class TokenService : ITokenService
    {
        // Basit bir bellek içi depolama (üretim için uygun değil)
        private static readonly ConcurrentDictionary<string, (Guid UserId, string Email, DateTime ExpiryDate)> _tokens = new ConcurrentDictionary<string, (Guid, string, DateTime)>();

        public Task<string> GenerateResetTokenAsync(Guid userId, string email)
        {
            var token = Guid.NewGuid().ToString();
            var expiryDate = DateTime.UtcNow.AddHours(1); // 1 saat geçerli
            _tokens[token] = (userId, email, expiryDate);
            return Task.FromResult(token);
        }

        public Task<bool> ValidateResetTokenAsync(string email, string token)
        {
            if (_tokens.TryGetValue(token, out var tokenInfo))
            {
                if (tokenInfo.Email == email && tokenInfo.ExpiryDate > DateTime.UtcNow)
                {
                    // Token geçerli, ancak üretimde tek kullanımlık olmalı ve burada silinmeli
                    // _tokens.TryRemove(token, out _);
                    return Task.FromResult(true);
                }
            }
            return Task.FromResult(false);
        }

        public Task InvalidateResetTokenAsync(string token)
        {
            _tokens.TryRemove(token, out _);
            return Task.CompletedTask;
        }
    }
}
