using System;

namespace DonationTrackingSystem.Domain.Events
{
    /// <summary>
    /// Domain olayları için temel arayüz
    /// </summary>
    public interface IDomainEvent
    {
        /// <summary>
        /// Olayın oluştuğu tarih
        /// </summary>
        DateTime OccurredOn { get; }

        /// <summary>
        /// Olayın benzersiz kimliği
        /// </summary>
        Guid EventId { get; }
    }
} 