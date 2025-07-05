using FluentValidation;
using DonationTrackingSystem.Application.Features.Bagis.Commands;
using DonationTrackingSystem.Domain.Enums;
using System;

namespace DonationTrackingSystem.Application.Validators
{
    /// <summary>
    /// CreateBagisCommand'in doğrulama kurallarını tanımlar.
    /// FluentValidation kütüphanesini kullanarak, komutun özelliklerinin
    /// geçerli ve tutarlı olmasını sağlar.
    /// </summary>
    public class CreateBagisCommandValidator : AbstractValidator<CreateBagisCommand>
    {
        public CreateBagisCommandValidator()
        {
            // BagisciAdi alanı için doğrulama kuralları
            RuleFor(x => x.BagisciAdi)
                // Alanın boş olmamasını sağlar.
                .NotEmpty().WithMessage("Bağışçı adı boş olamaz.")
                // Alanın maksimum 100 karakter olmasını sağlar.
                .MaximumLength(100).WithMessage("Bağışçı adı 100 karakterden uzun olamaz.");

            // Tutar alanı için doğrulama kuralları
            RuleFor(x => x.Tutar)
                // Tutarın 0'dan büyük olmasını zorunlu kılar.
                .GreaterThan(0).WithMessage("Tutar 0'dan büyük olmalıdır.")
                // Tutarın 1.000.000'dan küçük olmasını sağlar.
                .LessThan(1_000_000).WithMessage("Tutar 1.000.000'dan küçük olmalıdır.");

            // BagisTuru alanı için doğrulama kuralları
            RuleFor(x => x.BagisTuru)
                .IsInEnum().When(x => x.BagisTuru.HasValue).WithMessage("Geçersiz bağış türü.");

            // Tarih alanı için doğrulama kuralları
            RuleFor(x => x.Tarih)
                // Bağış tarihinin bugünden veya geçmiş bir tarihten olmasını sağlar.
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Bağış tarihi bugünden ileri bir tarih olamaz.");

            // Aciklama alanı için doğrulama kuralları
            RuleFor(x => x.Aciklama)
                // Açıklamanın maksimum 500 karakter olmasını sağlar.
                .MaximumLength(500).WithMessage("Açıklama 500 karakterden uzun olamaz.");
        }

        
    }
}