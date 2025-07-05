using System;

namespace DonationTrackingSystem.Domain.Common
{
    /// <summary>
    /// Validation sonucu için değer nesnesi.
    /// Birden fazla hata mesajı ve geçerlilik bilgisi içerir.
    /// </summary>
    public record ValidationResult(bool IsValid, string[] Errors)
    {
        /// <summary>
        /// Başarılı validation sonucu döndürür.
        /// </summary>
        public static ValidationResult Success() => new(true, Array.Empty<string>());
        /// <summary>
        /// Hatalı validation sonucu döndürür ve hata mesajlarını iletir.
        /// </summary>
        /// <param name="errors">Hata mesajları</param>
        public static ValidationResult Failure(params string[] errors) => new(false, errors);
    }
}