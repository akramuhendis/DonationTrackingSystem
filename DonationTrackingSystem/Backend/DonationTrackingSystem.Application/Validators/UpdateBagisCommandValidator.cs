using FluentValidation;
using DonationTrackingSystem.Application.Features.Bagis.Commands;
using DonationTrackingSystem.Domain.Enums;
using System;

namespace DonationTrackingSystem.Application.Validators
{
    /// <summary>
    /// UpdateBagisCommand için doğrulama kurallarını tanımlar.
    /// Bu sınıf, mevcut bir bağış kaydının güncellenmesi sırasında gönderilen verilerin
    /// geçerli ve iş kurallarına uygun olmasını sağlar.
    /// </summary>
    public class UpdateBagisCommandValidator : AbstractValidator<UpdateBagisCommand>
    {
        /// <summary>
        /// Validator sınıfının kurucu metodu. Tüm doğrulama kuralları burada tanımlanır.
        /// </summary>
        public UpdateBagisCommandValidator()
        {
            // Id alanı için doğrulama kuralı.
            RuleFor(x => x.Id)
                // Guid'in boş (Guid.Empty) olmamasını sağlar. Güncellenecek bağışın ID'si mutlaka geçerli olmalıdır.
                .NotEmpty().WithMessage("Güncellenecek bağış ID'si boş olamaz.");

            // BagisciAdi alanı için doğrulama kuralları.
            RuleFor(x => x.BagisciAdi)
                // Bağışçı adının boş olmamasını sağlar.
                .NotEmpty().WithMessage("Bağışçı adı boş olamaz.")
                // Bağışçı adının maksimum 100 karakter olmasını sınırlar.
                .MaximumLength(100).WithMessage("Bağışçı adı 100 karakterden uzun olamaz.");

            // Tutar alanı için doğrulama kuralları.
            RuleFor(x => x.Tutar)
                // Tutarın 0'dan büyük olmasını zorunlu kılar (negatif veya sıfır bağış olamaz).
                .GreaterThan(0).WithMessage("Tutar 0'dan büyük olmalıdır.")
                // Tutarın 1.000.000'dan küçük olmasını sağlar (mantıksız büyük bağışları engeller).
                .LessThan(1_000_000).WithMessage("Tutar 1.000.000'dan küçük olmalıdır.");

            // BagisTuru alanı için doğrulama kuralları
            RuleFor(x => x.BagisTuru)
                .IsInEnum().When(x => x.BagisTuru.HasValue).WithMessage("Geçersiz bağış türü.");

            // Tarih alanı için doğrulama kuralları.
            RuleFor(x => x.Tarih)
                // Bağış tarihinin bugünden veya geçmiş bir tarihten olmasını sağlar (gelecek tarihli bağışları engeller).
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Bağış tarihi bugünden ileri bir tarih olamaz.");

            // Aciklama alanı için doğrulama kuralları.
            RuleFor(x => x.Aciklama)
                // Açıklamanın maksimum 500 karakter olmasını sınırlar.
                .MaximumLength(500).WithMessage("Açıklama 500 karakterden uzun olamaz.");
        }

        
    }
}