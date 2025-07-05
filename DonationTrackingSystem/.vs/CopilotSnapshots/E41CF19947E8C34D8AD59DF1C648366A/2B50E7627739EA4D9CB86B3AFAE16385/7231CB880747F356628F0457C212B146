using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DonationTrackingSystem.Domain.Enums;

namespace DonationTrackingSystem.Domain.Entities
{
    /// <summary>
    /// Sistemdeki kullanıcıların bilgilerini tutan sınıf
    /// </summary>
    public class Kullanici
    {
        /// <summary>
        /// Kullanıcının benzersiz kimlik numarası
        /// </summary>
        public int Id { get; set; }

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
        public string Eposta { get; set; } = string.Empty;

        /// <summary>
        /// Kullanıcının şifresi
        /// </summary>
        public string Sifre { get; set; } = string.Empty;

        /// <summary>
        /// Kullanıcının sistemdeki rolü (örn: Admin, Kullanıcı)
        /// </summary>
        public Rol Rol { get; set; }

        /// <summary>
        /// Kullanıcının sisteme kayıt olduğu tarih
        /// </summary>
        public DateTime KayitTarihi { get; set; } = DateTime.Now;
    }
}
