using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DonationTrackingSystem.Domain.Common;

namespace DonationTrackingSystem.Domain.Entities
{
    /// <summary>
    /// Hesap planı (muhasebe hesap ağacı) bilgisini tutan entity.
    /// Her bir hesap planı, bir kod ve açıklama ile tanımlanır ve birden fazla muhasebe fişiyle ilişkilendirilebilir.
    /// </summary>
    public class HesapPilani : BaseEntity
    {
        public HesapPilani(string kod, string aciklama)
        {
            Kod = kod;
            Aciklama = aciklama;
        }

        // EF Core için parametresiz yapıcı metot
        private HesapPilani() { }

        /// <summary>
        /// Hesap planı kodu (ör: 100, 102, 120 gibi)
        /// </summary>
        public string Kod { get; set; } = string.Empty;

        /// <summary>
        /// Hesap planı açıklaması (ör: Kasa, Banka, Alıcılar, vb.)
        /// </summary>
        public string Aciklama { get; set; } = string.Empty;

        /// <summary>
        /// Hesap planına bağlı muhasebe fişleri (ilişkili kayıtlar)
        /// </summary>
        public ICollection<MuhasebeFis>? Fisler { get; set; }
    }
}
