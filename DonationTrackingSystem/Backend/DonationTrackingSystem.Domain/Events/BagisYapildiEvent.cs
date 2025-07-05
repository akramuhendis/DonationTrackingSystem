using System;
using DonationTrackingSystem.Domain.Entities;
using DonationTrackingSystem.Domain.ValueObjects;

namespace DonationTrackingSystem.Domain.Events
{
        /// <summary>
    /// Bağış yapıldığında tetiklenen domain olayı
    /// </summary>
    public class BagisYapildiEvent : IDomainEvent
    {
        // Olayın benzersiz kimliği (her event için farklı bir Guid üretilir)
        public Guid EventId { get; } = Guid.NewGuid();

        // Olayın gerçekleştiği zaman (UTC)
        public DateTime OccurredOn { get; } = DateTime.UtcNow;

        // Bağışın kimliği
        public Guid BagisId { get; }

        // Bağışçının kimliği
        public Guid BagisciId { get; }

        // Bağış tutarı artık Money tipinde
        public Money Tutar { get; }

        // Bağışçının adı ve soyadı
        public string BagisciAdi { get; }

        /// <summary>
        /// Bağış ve bağışçı nesneleriyle olayı başlatır, ilgili bilgileri doldurur.
        /// </summary>
        /// <param name="bagis">Bağış nesnesi</param>
        /// <param name="bagisci">Bağışçı nesnesi</param>
        public BagisYapildiEvent(Bagis bagis, Bagisci bagisci)
        {
            // Null kontrolü ekleniyor
            if (bagis == null)
                throw new ArgumentNullException(nameof(bagis));
            if (bagisci == null)
                throw new ArgumentNullException(nameof(bagisci));
            // Bağış ve bağışçıdan gerekli alanlar alınır
            BagisId = bagis.Id;
            BagisciId = bagisci.Id;
            // Money tipine dönüştürülerek atanıyor (varsayılan para birimi: TRY)
            Tutar = new Money(bagis.Tutar, "TRY");
            BagisciAdi = bagisci.AdSoyad;
        }
    }
    
}