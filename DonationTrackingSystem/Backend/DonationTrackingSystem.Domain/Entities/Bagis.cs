using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DonationTrackingSystem.Domain.Common;
using DonationTrackingSystem.Domain.Enums;

namespace DonationTrackingSystem.Domain.Entities
{
    /// <summary>
    /// Bağış işlemlerinin bilgilerini tutan sınıf
    /// </summary>
    public class Bagis : BaseEntity
    {
        public Bagis(BagisTuru bagisTuru, decimal tutar, DateTime bagisTarihi, Guid bagisciId, string? aciklama = null)
        {
            BagisTuru = bagisTuru;
            Tutar = tutar;
            BagisTarihi = bagisTarihi;
            BagisciId = bagisciId;
            Aciklama = aciklama;
        }

        // EF Core için parametresiz yapıcı metot
        private Bagis() { }

        /// <summary>
        /// Bağışın türü (ör: nakit, eşya, hizmet)
        /// </summary>
        public BagisTuru BagisTuru { get; set; }

        /// <summary>
        /// Bağışın tutarı
        /// </summary>
        public decimal Tutar { get; set; }

        /// <summary>
        /// Bağış ile ilgili açıklama
        /// </summary>
        public string? Aciklama { get; set; }

        /// <summary>
        /// Bağışın yapıldığı tarih
        /// </summary>
        public DateTime BagisTarihi { get; set; }

        /// <summary>
        /// Bağışı yapan bağışçının kimlik numarası (foreign key)
        /// </summary>
        public Guid BagisciId { get; set; }

        /// <summary>
        /// Bağışı yapan bağışçıya ait bilgiler (navigation property)
        /// </summary>
        public Bagisci? Bagisci { get; set; }
    }
}
