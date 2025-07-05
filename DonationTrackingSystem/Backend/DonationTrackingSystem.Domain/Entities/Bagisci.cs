using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DonationTrackingSystem.Domain.Common;

namespace DonationTrackingSystem.Domain.Entities
{
    /// <summary>
    /// Bağış yapan kişilerin bilgilerini tutan sınıf
    /// </summary>
    public class Bagisci : BaseEntity
    {
        public Bagisci(string adSoyad, string telefon, string eposta)
        {
            AdSoyad = adSoyad;
            Telefon = telefon;
            Eposta = eposta;
        }

        // EF Core için parametresiz yapıcı metot
        private Bagisci() { }

        /// <summary>
        /// Bağışçının ad ve soyad bilgisi
        /// </summary>
        public string AdSoyad { get; set; } = string.Empty;

        /// <summary>
        /// Bağışçının iletişim için telefon numarası
        /// </summary>
        public string Telefon { get; set; } = string.Empty;

        /// <summary>
        /// Bağışçının e-posta adresi
        /// </summary>
        public string Eposta { get; set; } = string.Empty;

        /// <summary>
        /// Bağışçının yapmış olduğu bağışların listesi
        /// Null olabilir çünkü yeni kayıt olan bir bağışçının henüz bağışı olmayabilir
        /// </summary>
        public ICollection<Bagis>? Bagislar { get; set; }
    }
}
