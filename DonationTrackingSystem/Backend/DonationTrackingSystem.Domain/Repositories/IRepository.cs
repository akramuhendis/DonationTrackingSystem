using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using DonationTrackingSystem.Domain.Common;

namespace DonationTrackingSystem.Domain.Repositories
{
    /// <summary>
    /// Tüm repository'ler için temel arayüz
    /// </summary>
    /// <typeparam name="T">Entity tipi</typeparam>
    public interface IRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// Varlığı ID'ye göre getirir
        /// </summary>
        /// <param name="id">Entity ID'si</param>
        /// <param name="includeProperties">İlişkili entity'leri belirten propertyler</param>
        /// <param name="cancellationToken">İptal token'ı</param>
        Task<T?> GetByIdAsync(Guid id, string[]? includeProperties = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sayfalanmış varlıkları getirir
        /// </summary>
        /// <param name="pageNumber">Sayfa numarası (1'den başlar)</param>
        /// <param name="pageSize">Sayfa başına kayıt sayısı</param>
        /// <param name="orderBy">Sıralama fonksiyonu</param>
        Task<(IEnumerable<T> Items, int TotalCount)> GetPagedAsync(
            int pageNumber,
            int pageSize,
            Expression<Func<T, object>>? orderBy = null,
            bool ascending = true,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Koşula göre varlıkları getirir
        /// </summary>
        Task<IEnumerable<T>> FindAsync(
            Expression<Func<T, bool>> predicate,
            string[]? includeProperties = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Varlık ekler
        /// </summary>
        /// <exception cref="ArgumentNullException">entity null ise fırlatılır</exception>
        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Birden fazla varlık ekler
        /// </summary>
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// Varlık günceller
        /// </summary>
        /// <exception cref="ArgumentNullException">entity null ise fırlatılır</exception>
        Task UpdateAsync(T entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Varlık siler
        /// </summary>
        /// <exception cref="InvalidOperationException">Silinecek varlık bulunamadığında fırlatılır</exception>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Varlığın var olup olmadığını kontrol eder
        /// </summary>
        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);

        }
}