using System;
using System.Linq.Expressions;
using DonationTrackingSystem.Domain.Common;

namespace DonationTrackingSystem.Domain.Specifications
{
    /// <summary>
    /// Specification pattern için temel arayüz.
    /// Sorgu kriterlerini, eager loading (include), sıralama ve sayfalama gibi gelişmiş sorgu özelliklerini bir arada tanımlar.
    /// Repository pattern ile birlikte kullanılarak, tekrar kullanılabilir ve test edilebilir sorgular oluşturulmasını sağlar.
    /// </summary>
    /// <typeparam name="T">Entity tipi (BaseEntity'den türemeli)</typeparam>
    public interface ISpecification<T> where T : BaseEntity
    {
        /// <summary>
        /// Sorgunun ana kriterini (where şartı) temsil eden expression.
        /// Örneğin: x => x.IsActive && x.Amount > 100
        /// </summary>
        Expression<Func<T, bool>> Criteria { get; }

        /// <summary>
        /// Eager loading için include edilecek navigation property'lerin listesi.
        /// Örneğin: x => x.Bagisci, x => x.Bagislar
        /// </summary>
        List<Expression<Func<T, object>>> Includes { get; }

        /// <summary>
        /// Sorgu sonucunun artan şekilde sıralanacağı property.
        /// Örneğin: x => x.CreatedAt
        /// </summary>
        Expression<Func<T, object>>? OrderBy { get; }

        /// <summary>
        /// Sorgu sonucunun azalan şekilde sıralanacağı property.
        /// Örneğin: x => x.Amount
        /// </summary>
        Expression<Func<T, object>>? OrderByDescending { get; }

        /// <summary>
        /// Sayfalama için atlanacak (skip edilecek) kayıt sayısı.
        /// Örneğin: 20 kayıt atla (ikinci sayfa için)
        /// </summary>
        int Skip { get; }

        /// <summary>
        /// Sayfalama için alınacak kayıt sayısı (take).
        /// Örneğin: 10 kayıt al (sayfa başına 10 kayıt için)
        /// </summary>
        int Take { get; }

        /// <summary>
        /// Sayfalama (skip/take) kullanılıp kullanılmayacağını belirten işaret.
        /// </summary>
        bool IsPagingEnabled { get; }
    }
}