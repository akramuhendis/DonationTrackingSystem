using System;
using System.Threading.Tasks;
using DonationTrackingSystem.Domain.Entities;
using DonationTrackingSystem.Domain.ValueObjects;

namespace DonationTrackingSystem.Domain.Services
{
    /// <summary>
    /// Bağış işlemleri için domain servis arayüzü.
    /// Bağış oluşturma, doğrulama, limit kontrolü ve istatistik hesaplama gibi iş kurallarını içerir.
    /// </summary>
    public interface IBagisService
    {
        /// <summary>
        /// Yeni bağış oluşturur ve iş kurallarını uygular.
        /// Bağışçı, tutar ve açıklama parametreleriyle yeni bir bağış kaydı oluşturur.
        /// </summary>
        /// <param name="bagisci">Bağışı yapan kişi</param>
        /// <param name="tutar">Bağış miktarı (Money value object)</param>
        /// <param name="aciklama">Bağış açıklaması (opsiyonel)</param>
        /// <returns>Oluşturulan bağış nesnesi</returns>
        Task<Bagis> CreateBagisAsync(Bagisci bagisci, Money tutar, string? aciklama);

        /// <summary>
        /// Bağış geçerliliğini kontrol eder.
        /// İş kurallarına göre bağışın uygun olup olmadığını döner.
        /// </summary>
        /// <param name="bagis">Kontrol edilecek bağış nesnesi</param>
        /// <returns>Geçerliyse true, değilse false</returns>
        Task<bool> ValidateBagisAsync(Bagis bagis);

        /// <summary>
        /// Bağışçının bağış limitini kontrol eder.
        /// Belirli bir bağışçının yeni bağış ile limiti aşıp aşmadığını kontrol eder.
        /// </summary>
        /// <param name="bagisciId">Bağışçı kimliği</param>
        /// <param name="yeniTutar">Yeni bağış miktarı</param>
        /// <returns>Limit aşılmıyorsa true, aşılıyorsa false</returns>
        Task<bool> CheckBagisciLimitAsync(Guid bagisciId, Money yeniTutar);

        /// <summary>
        /// Belirli bir tarih aralığı için bağış istatistiklerini hesaplar.
        /// Toplam bağış sayısı, toplam tutar, ortalama tutar ve aktif bağışçı sayısı gibi verileri döner.
        /// </summary>
        /// <param name="baslangic">Başlangıç tarihi</param>
        /// <param name="bitis">Bitiş tarihi</param>
        /// <returns>Bağış istatistikleri nesnesi</returns>
        Task<BagisIstatistikleri> GetBagisIstatistikleriAsync(DateTime baslangic, DateTime bitis);
    }

    
}