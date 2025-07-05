using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DonationTrackingSystem.Domain.Common;
using DonationTrackingSystem.Domain.Enums;
using DonationTrackingSystem.Domain.ValueObjects;

namespace DonationTrackingSystem.Domain.Entities
{
    /// <summary>
    /// Sistemdeki kullanıcıların bilgilerini tutan sınıf
    /// </summary>
    public class Kullanici : BaseEntity
    {
        public Kullanici(string ad, string soyad, Email eposta, string sifreHash, Rol rol)
        {
            Ad = ad;
            Soyad = soyad;
            Eposta = eposta;
            SifreHash = sifreHash;
            Rol = rol;
            KayitTarihi = DateTime.UtcNow; // Kayıt tarihi UTC olarak ayarlandı
        }

        // EF Core için parametresiz yapıcı metot
        private Kullanici() { }

        /// <summary>
        /// Kullanıcının adı
        /// </summary>
        public string Ad { get; set; }  = string.Empty;

        /// <summary>
        /// Kullanıcının soyadı
        /// </summary>
        public string Soyad { get; set; } = string.Empty;

        /// <summary>
        /// Kullanıcının e-posta adresi
        /// </summary>
        public required Email Eposta { get; init; } // Email değer nesnesi olarak değiştirildi

        /// <summary>
        /// Kullanıcının şifresinin hashlenmiş hali
        /// </summary>
        public string SifreHash { get; set; } = string.Empty;

        /// <summary>
        /// Kullanıcının sistemdeki rolü (örn: Admin, Kullanıcı)
        /// </summary>
        public Rol Rol { get; set; }

        /// <summary>
        /// Kullanıcının sisteme kayıt olduğu tarih
        /// </summary>
        public DateTime KayitTarihi { get; set; }
    }
}
