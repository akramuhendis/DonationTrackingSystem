using DonationTrackingSystem.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DonationTrackingSystem.Domain.Repositories
{
    public interface IKullaniciRepository : IRepository<Kullanici>
    {
        // Kullanici'ye özgü metodlar buraya eklenebilir
        Task<List<Kullanici>> GetAllAsync(); // Tüm kullanıcıları getiren metot
        Task<(List<Kullanici> Users, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize, string? emailFilter, DonationTrackingSystem.Domain.Enums.Rol? rolFilter, CancellationToken cancellationToken = default);
        Task<Kullanici?> GetByEmailAsync(string email); // E-posta ile kullanıcı getiren metot
        Task<Kullanici?> GetByEmailAndPasswordAsync(string email, string passwordHash); // E-posta ve şifre hash ile kullanıcı getiren metot
    }
}