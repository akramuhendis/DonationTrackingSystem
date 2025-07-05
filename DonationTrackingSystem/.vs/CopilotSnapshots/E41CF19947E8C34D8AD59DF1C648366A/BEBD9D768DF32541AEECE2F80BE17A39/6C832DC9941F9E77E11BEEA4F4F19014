using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationTrackingSystem.Domain.Entities
{
    /// <summary>
    /// Kasa veya banka hesaplarının bilgilerini tutan sınıf
    /// </summary>
    public class KasaBanka
    {
        /// <summary>
        /// Kasa veya banka kaydının benzersiz kimlik numarası
        /// </summary>
        public Guid Id { get; set; }

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
        /// True: Kasa, False: Banka
        /// </summary>
        public bool Kasami { get; set; }
    }
}
