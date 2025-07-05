using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DonationTrackingSystem.Domain.Entities;
using DonationTrackingSystem.Domain.Enums;
using DonationTrackingSystem.Domain.ValueObjects;

namespace DonationTrackingSystem.Domain.Repositories
{
    /// <summary>
    /// Bağış işlemleri için özel repository arayüzü
    /// </summary>
    public interface IBagisRepository : IRepository<Bagis>
    {
        /// <summary>
        /// Belirli bir bağışçının tüm bağışlarını getirir
        /// </summary>
        Task<IEnumerable<Bagis>> GetBagislarByBagisciIdAsync(Guid bagisciId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Belirli bir tarih aralığındaki bağışları getirir
        /// </summary>
        Task<IEnumerable<Bagis>> GetBagislarByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);

        /// <summary>
        /// Belirli bir bağış türündeki bağışları getirir
        /// </summary>
        Task<IEnumerable<Bagis>> GetBagislarByTypeAsync(BagisTuru bagisTuru, CancellationToken cancellationToken = default);

        /// <summary>
        /// Toplam bağış miktarını hesaplar
        /// </summary>
        Task<Money> GetTotalBagisAmountAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Belirli bir bağışçının toplam bağış miktarını hesaplar
        /// </summary>
        Task<Money> GetTotalBagisAmountByBagisciAsync(Guid bagisciId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Tüm bağışları getirir
        /// </summary>
        Task<IEnumerable<Bagis>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}