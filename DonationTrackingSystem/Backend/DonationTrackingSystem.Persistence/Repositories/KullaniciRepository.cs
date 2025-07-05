using DonationTrackingSystem.Domain.Entities;
using DonationTrackingSystem.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DonationTrackingSystem.Domain.Enums; // For Rol enum
using System.Threading; // For CancellationToken

namespace DonationTrackingSystem.Persistence.Repositories
{
    public class KullaniciRepository : GenericRepository<Kullanici>, IKullaniciRepository
    {
        public KullaniciRepository(DbContext context) : base(context)
        {
        }

        public async Task<List<Kullanici>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<Kullanici?> GetByEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Eposta.Value == email); // Access Value property of Email
        }

        public async Task<Kullanici?> GetByEmailAndPasswordAsync(string email, string passwordHash)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Eposta.Value == email && u.SifreHash == passwordHash); // Access Value property of Email
        }

        public async Task<(List<Kullanici> Users, int TotalCount)> GetPagedAsync(
            int pageNumber,
            int pageSize,
            string? emailFilter,
            Rol? rolFilter,
            CancellationToken cancellationToken = default)
        {
            IQueryable<Kullanici> query = _dbSet;

            if (!string.IsNullOrWhiteSpace(emailFilter))
            {
                query = query.Where(u => u.Eposta.Value.Contains(emailFilter)); // Filter by Email Value
            }

            if (rolFilter.HasValue)
            {
                query = query.Where(u => u.Rol == rolFilter.Value); // Filter by Rol
            }

            var totalCount = await query.CountAsync(cancellationToken);

            var users = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (users, totalCount);
        }
    }
}