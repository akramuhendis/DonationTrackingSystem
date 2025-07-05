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
    /// Muhasebe işlemlerinin fiş kayıtlarını tutan sınıf
    /// </summary>
    public class MuhasebeFis : BaseEntity
    {
        public MuhasebeFis(string fisNo, string aciklama, decimal tutar, DateTime tarih, IslemTuru islemTuru, Guid hesapPlaniId)
        {
            FisNo = fisNo;
            Aciklama = aciklama;
            Tutar = tutar;
            Tarih = tarih;
            IslemTuru = islemTuru;
            HesapPlaniId = hesapPlaniId;
        }

        // EF Core için parametresiz yapıcı metot
        private MuhasebeFis() { }

        /// <summary>
        /// Muhasebe fişinin belge numarası
        /// </summary>
        public string FisNo { get; set; } = string.Empty;

        /// <summary>
        /// Muhasebe fişi ile ilgili açıklama bilgisi
        /// </summary>
        public string Aciklama { get; set; } = string.Empty;

        /// <summary>
        /// İşlemin parasal tutarı
        /// </summary>
        public decimal Tutar { get; set; }

        /// <summary>
        /// Muhasebe fişinin oluşturulma tarihi
        /// </summary>
        public DateTime Tarih { get; set; }

        /// <summary>
        /// İşlemin gelir mi gider mi olduğunu belirten alan
        /// </summary>
        public IslemTuru IslemTuru { get; set; }

        /// <summary>
        /// İlişkili hesap planının benzersiz kimlik numarası
        /// </summary>
        public Guid HesapPlaniId { get; set; }

        /// <summary>
        /// Muhasebe fişinin bağlı olduğu hesap planı
        /// Navigation property
        /// </summary>
        public HesapPilani? HesapPlani { get; set; }
    }
}
