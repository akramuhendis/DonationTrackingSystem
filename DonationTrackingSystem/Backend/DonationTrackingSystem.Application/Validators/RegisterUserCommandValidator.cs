using FluentValidation;
using DonationTrackingSystem.Application.Features.Kullanici.Commands;
using DonationTrackingSystem.Application.DTOs;
using DonationTrackingSystem.Domain.Enums;
using System;

namespace DonationTrackingSystem.Application.Validators
{
    /// <summary>
    /// RegisterUserCommand'in doğrulama kurallarını tanımlar.
    /// Bu sınıf, yeni bir kullanıcı kaydı oluşturulurken gönderilen verilerin
    /// geçerli ve iş kurallarına uygun olmasını sağlar.
    /// </summary>
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        /// <summary>
        /// Validator sınıfının kurucu metodu. Tüm doğrulama kuralları burada tanımlanır.
        /// </summary>
        public RegisterUserCommandValidator()
        {
            // UserRegister DTO'sunun boş olmamasını sağlar.
            RuleFor(x => x.UserRegister).NotNull().WithMessage("Kayıt bilgileri boş olamaz.");

            // AdSoyad alanı için doğrulama kuralları.
            RuleFor(x => x.UserRegister.AdSoyad)
                // Alanın boş olmamasını sağlar.
                .NotEmpty().WithMessage("Ad soyad boş olamaz.")
                // Alanın maksimum 100 karakter olmasını sınırlar.
                .MaximumLength(100).WithMessage("Ad soyad 100 karakterden uzun olamaz.");

            // Email alanı için doğrulama kuralları.
            RuleFor(x => x.UserRegister.Email)
                // Alanın boş olmamasını sağlar.
                .NotEmpty().WithMessage("E-posta adresi boş olamaz.")
                // Girilen metnin geçerli bir e-posta formatında olup olmadığını kontrol eder.
                .EmailAddress().WithMessage("Lütfen geçerli bir e-posta adresi girin.");

            // Sifre alanı için güçlü parola doğrulama kuralları.
            RuleFor(x => x.UserRegister.Sifre)
                // Şifrenin boş olmamasını sağlar.
                .NotEmpty().WithMessage("Şifre boş olamaz.")
                // Minimum 8 karakter uzunluğunu zorunlu kılar.
                .MinimumLength(8).WithMessage("Şifre en az 8 karakter olmalıdır.")
                // En az bir büyük harf [A-Z] içermesini zorunlu kılar.
                .Matches("[A-Z]").WithMessage("Şifre en az bir büyük harf içermelidir.")
                // En az bir küçük harf [a-z] içermesini zorunlu kılar.
                .Matches("[a-z]").WithMessage("Şifre en az bir küçük harf içermelidir.")
                // En az bir rakam [0-9] içermesini zorunlu kılar.
                .Matches("[0-9]").WithMessage("Şifre en az bir rakam içermelidir.")
                // Harf ve rakam olmayan en az bir özel karakter içermesini zorunlu kılar.
                .Matches("[^a-zA-Z0-9]").WithMessage("Şifre en az bir özel karakter içermelidir.");

            // Rol alanı için doğrulama kuralı.
            RuleFor(x => x.UserRegister.Rol)
                .IsInEnum().WithMessage("Geçersiz rol değeri.");
        }

        
    }
}