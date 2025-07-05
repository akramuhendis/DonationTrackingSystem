using DonationTrackingSystem.Domain.ValueObjects;

namespace DonationTrackingSystem.Domain.ValueObjects
{
    /// <summary>
    /// Bağış istatistikleri için değer nesnesi.
    /// Toplam bağış sayısı, toplam tutar, ortalama tutar ve aktif bağışçı sayısı gibi istatistikleri içerir.
    /// </summary>
    public record BagisIstatistikleri(
        int ToplamBagisSayisi,
        Money ToplamBagisTutari,
        decimal OrtalamaBagisTutari,
        int AktifBagisciSayisi
    );
}