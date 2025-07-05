using System;

namespace DonationTrackingSystem.Application.DTOs
{
    // BagisDto, bağış işlemlerinin veri transferi için kullanılan bir Data Transfer Object (DTO) sınıfıdır.
    // Bu sınıf, katmanlar arası veri taşırken kullanılır ve genellikle API ile istemci arasında veri iletimini kolaylaştırır.
    public class BagisDto
    {
        // Bağışın benzersiz kimliği
        public Guid Id { get; set; }
        // Bağışı yapan kişinin adı. Nullable olabilir.
        public string? BagisciAdi { get; set; }
        // Bağışın tutarı (miktarı). Ondalıklı değer alabilir.
        public decimal Tutar { get; set; }
        // Bağış türü (ör. nakit, eşya vb.). Nullable olabilir.
        public DonationTrackingSystem.Domain.Enums.BagisTuru? BagisTuru { get; set; }
        // Bağışın yapıldığı tarih. Nullable olabilir.
        public DateTime? Tarih { get; set; }
        // Bağış ile ilgili açıklama veya not. Nullable olabilir.
        public string? Aciklama { get; set; }
    }
}