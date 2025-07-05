using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationTrackingSystem.Domain.Entities
{
    /// <summary>
    /// Bağış yapan kişilerin bilgilerini tutan sınıf
    /// </summary>
    public class Bagisci
    {
        /// <summary>
        /// Bağışçının benzersiz kimlik numarası
        /// </summary>
        public Guid Id { get; set; }

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
