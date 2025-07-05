using FluentValidation;
using DonationTrackingSystem.Application.Features.Kullanici.Commands;
using DonationTrackingSystem.Domain.Enums;
using System;

namespace DonationTrackingSystem.Application.Validators
{
    /// <summary>
    /// UpdateUserCommand için doğrulama kurallarını tanımlar.
    /// </summary>
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Kullanıcı ID'si boş olamaz.");

            RuleFor(x => x.AdSoyad)
                .NotEmpty().WithMessage("Ad soyad boş olamaz.")
                .MaximumLength(100).WithMessage("Ad soyad 100 karakterden uzun olamaz.");

            RuleFor(x => x.Eposta)
                .NotEmpty().WithMessage("E-posta adresi boş olamaz.")
                .EmailAddress().WithMessage("Lütfen geçerli bir e-posta adresi girin.");

            RuleFor(x => x.Rol)
                .Must(BeValidRol).WithMessage("Geçersiz rol değeri.");
        }

        private bool BeValidRol(DonationTrackingSystem.Domain.Enums.Rol rol)
        {
            // Rol enum olduğu için doğrudan geçerli olup olmadığını kontrol edebiliriz.
            // Enum.IsDefined metodu, belirtilen enum değerinin enum'da tanımlı olup olmadığını kontrol eder.
            return Enum.IsDefined(typeof(Rol), rol);
        }
    }
}