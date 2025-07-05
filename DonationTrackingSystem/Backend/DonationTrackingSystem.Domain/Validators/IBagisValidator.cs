using System.Threading.Tasks;
using DonationTrackingSystem.Domain.Entities;
using DonationTrackingSystem.Domain.Common;

namespace DonationTrackingSystem.Domain.Validators
{
    /// <summary>
    /// Bağış işlemleri için validation arayüzü.
    /// Bağış tutarı, bağışçı, tür ve tarih gibi alanların iş kurallarına uygunluğunu kontrol eder.
    /// </summary>
    public interface IBagisValidator
    {
        /// <summary>
        /// Bağış tutarının geçerli olup olmadığını kontrol eder.
        /// Örneğin: tutar negatif mi, minimum/maximum sınırda mı?
        /// </summary>
        /// <param name="tutar">Kontrol edilecek bağış tutarı</param>
        /// <returns>Geçerliyse true, değilse false</returns>
        Task<bool> ValidateTutarAsync(decimal tutar);

        /// <summary>
        /// Bağışçının bağış yapabilir durumda olup olmadığını kontrol eder.
        /// Örneğin: bağışçı aktif mi, engelli mi, limiti dolmuş mu?
        /// </summary>
        /// <param name="bagisciId">Bağışçı kimliği</param>
        /// <returns>Bağış yapabiliyorsa true, değilse false</returns>
        Task<bool> ValidateBagisciAsync(Guid bagisciId);

        /// <summary>
        /// Bağış türünün geçerli olup olmadığını kontrol eder.
        /// Örneğin: sistemde tanımlı bir tür mü?
        /// </summary>
        /// <param name="bagisTuru">Bağış türü enum değeri</param>
        /// <returns>Geçerliyse true, değilse false</returns>
        Task<bool> ValidateBagisTuruAsync(Enums.BagisTuru bagisTuru);

        /// <summary>
        /// Bağış tarihinin geçerli olup olmadığını kontrol eder.
        /// Örneğin: gelecek bir tarih mi, çok eski bir tarih mi?
        /// </summary>
        /// <param name="bagisTarihi">Bağış tarihi</param>
        /// <returns>Geçerliyse true, değilse false</returns>
        Task<bool> ValidateBagisTarihiAsync(System.DateTime bagisTarihi);

        /// <summary>
        /// Tüm validation kurallarını topluca kontrol eder.
        /// Bağış nesnesinin tüm alanlarını iş kurallarına göre doğrular.
        /// </summary>
        /// <param name="bagis">Kontrol edilecek bağış nesnesi</param>
        /// <returns>Validation sonucu (geçerli mi, hata mesajları)</returns>
        Task<ValidationResult> ValidateBagisAsync(Bagis bagis);
    }

    
}