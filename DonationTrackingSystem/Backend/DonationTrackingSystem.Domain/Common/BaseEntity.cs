using System;
using System.Collections.Generic;
using System.Linq;
using DonationTrackingSystem.Domain.Events;

namespace DonationTrackingSystem.Domain.Common
{
    /// <summary>
    /// Tüm entity'ler için temel sınıf
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Entity'nin benzersiz kimliği
        /// </summary>
        public Guid Id { get; protected set; }

        /// <summary>
        /// Oluşturulma tarihi (UTC)
        /// </summary>
        public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;

        /// <summary>
        /// Son güncelleme tarihi (UTC)
        /// </summary>
        public DateTime? UpdatedAt { get; protected set; }

        /// <summary>
        /// Entity'nin aktif/pasif durumu
        /// </summary>
        public bool IsActive { get; protected set; } = true;

        /// <summary>
        /// Entity'nin silinip silinmediği
        /// </summary>
        public bool IsDeleted { get; protected set; }

        private readonly List<IDomainEvent> _domainEvents = new();

        /// <summary>
        /// Entity'yi günceller
        /// </summary>
        protected void Update()
        {
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Entity'yi siler (soft delete)
        /// </summary>
        protected void Delete()
        {
            IsDeleted = true;
            IsActive = false;
            Update();
        }

        /// <summary>
        /// Domain olayını ekler.
        /// </summary>
        /// <param name="event">Eklenecek domain olayı.</param>
        public void AddDomainEvent(IDomainEvent @event)
        {
            _domainEvents.Add(@event);
        }

        /// <summary>
        /// Domain olayını kaldırır.
        /// </summary>
        /// <param name="event">Kaldırılacak domain olayı.</param>
        public void RemoveDomainEvent(IDomainEvent @event)
        {
            _domainEvents.Remove(@event);
        }

        /// <summary>
        /// Tüm domain olaylarını temizler.
        /// </summary>
        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        /// <summary>
        /// Yayımlanmamış domain olaylarını döndürür.
        /// </summary>
        /// <returns>Yayımlanmamış domain olaylarının listesi.</returns>
        public IReadOnlyCollection<IDomainEvent> GetDomainEvents() => _domainEvents.AsReadOnly();
    }
}
