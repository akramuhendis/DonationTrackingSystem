using FluentValidation;
using DonationTrackingSystem.Application.Features.Kullanici.Commands;
using DonationTrackingSystem.Application.DTOs;

namespace DonationTrackingSystem.Application.Validators
{
    /// <summary>
    /// LoginUserCommand'in doğrulama kurallarını tanımlar.
    /// Bu sınıf, AbstractValidator sınıfından kalıtım alarak FluentValidation kütüphanesinin
    /// güçlü ve okunabilir doğrulama yeteneklerini kullanır.
    /// </summary>
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        /// <summary>
        /// Validator sınıfının kurucu metodu. Doğrulama kuralları burada tanımlanır.
        /// </summary>
        public LoginUserCommandValidator()
        {
            // UserLogin DTO'sunun boş olmamasını sağlar.
            RuleFor(x => x.UserLogin).NotNull().WithMessage("Giriş bilgileri boş olamaz.");

            // Email alanı için bir doğrulama kuralı zinciri oluşturulur.
            RuleFor(x => x.UserLogin.Email)
                // 1. Kural: Email alanının boş veya null olmamasını sağlar.
                // WithMessage ile standart hata mesajı yerine özel bir mesaj atanır.
                .NotEmpty().WithMessage("E-posta adresi boş olamaz.")

                // 2. Kural: Girilen metnin geçerli bir e-posta adresi formatında olup olmadığını kontrol eder.
                // Örneğin, 'kullanici@site.com' formatını zorunlu kılar.
                .EmailAddress().WithMessage("Lütfen geçerli bir e-posta adresi girin.");

            // Sifre alanı için bir doğrulama kuralı zinciri oluşturulur.
            RuleFor(x => x.UserLogin.Sifre)
                .NotEmpty().WithMessage("Şifre boş olamaz.")
                .MinimumLength(8).WithMessage("Şifre en az 8 karakter olmalıdır.")
                .Matches("[A-Z]").WithMessage("Şifre en az bir büyük harf içermelidir.")
                .Matches("[a-z]").WithMessage("Şifre en az bir küçük harf içermelidir.")
                .Matches("[0-9]").WithMessage("Şifre en az bir rakam içermelidir.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Şifre en az bir özel karakter içermelidir.");
        }
    }
} 