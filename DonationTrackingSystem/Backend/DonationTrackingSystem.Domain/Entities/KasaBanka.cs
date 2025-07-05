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
    /// Kasa veya banka hesaplarının bilgilerini tutan sınıf
    /// </summary>
    public class KasaBanka : BaseEntity
    {
        public KasaBanka(string ad, decimal bakiye, HesapTuru hesapTuru)
        {
            Ad = ad;
            Bakiye = bakiye;
            HesapTuru = hesapTuru;
        }

        // EF Core için parametresiz yapıcı metot
        private KasaBanka() { }

        /// <summary>
        /// Kasa veya banka adını tutar
        /// </summary>
        public string Ad { get; set; } = string.Empty;

        /// <summary>
        /// Kasa veya bankadaki mevcut bakiye
        /// </summary>
        public decimal Bakiye { get; set; }

        /// <summary>
        /// Kaydın kasa mı yoksa banka mı olduğunu belirten alan
        /// </summary>
        public HesapTuru HesapTuru { get; set; }
    }
}
