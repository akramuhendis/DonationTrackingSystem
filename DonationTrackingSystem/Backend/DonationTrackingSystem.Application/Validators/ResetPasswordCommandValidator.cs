using FluentValidation;
using DonationTrackingSystem.Application.Features.Kullanici.Commands;

namespace DonationTrackingSystem.Application.Validators
{
    /// <summary>
    /// ResetPasswordCommand için doğrulama kurallarını tanımlar.
    /// Bu sınıf, kullanıcının şifre sıfırlama isteği gönderirken sağladığı verilerin
    /// geçerli ve güvenli olmasını sağlamak için FluentValidation kurallarını kullanır.
    /// </summary>
    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
    {
        /// <summary>
        /// Validator sınıfının kurucu metodu. Tüm doğrulama kuralları burada tanımlanır.
        /// </summary>
        public ResetPasswordCommandValidator()
        {
            // Email alanı için doğrulama kuralları.
            RuleFor(x => x.Email)
                // E-posta adresinin boş olmamasını sağlar.
                .NotEmpty().WithMessage("E-posta adresi boş olamaz.")
                // Girilen metnin geçerli bir e-posta adresi formatında olup olmadığını kontrol eder.
                .EmailAddress().WithMessage("Lütfen geçerli bir e-posta adresi girin.");

            // ResetToken alanı için doğrulama kuralı.
            RuleFor(x => x.ResetToken)
                // Sıfırlama token'ının boş olmamasını sağlar.
                .NotEmpty().WithMessage("Sıfırlama token'ı boş olamaz.");

            // NewPassword (Yeni Şifre) alanı için güçlü parola doğrulama kuralları.
            RuleFor(x => x.NewPassword)
                // Yeni şifrenin boş olmamasını sağlar.
                .NotEmpty().WithMessage("Yeni şifre boş olamaz.")
                // Minimum 8 karakter uzunluğunu zorunlu kılar.
                .MinimumLength(8).WithMessage("Yeni şifre en az 8 karakter olmalıdır.")
                // En az bir büyük harf [A-Z] içermesini zorunlu kılar.
                .Matches("[A-Z]").WithMessage("Yeni şifre en az bir büyük harf içermelidir.")
                // En az bir küçük harf [a-z] içermesini zorunlu kılar.
                .Matches("[a-z]").WithMessage("Yeni şifre en az bir küçük harf içermelidir.")
                // En az bir rakam [0-9] içermesini zorunlu kılar.
                .Matches("[0-9]").WithMessage("Yeni şifre en az bir rakam içermelidir.")
                // Harf ve rakam olmayan en az bir özel karakter içermesini zorunlu kılar.
                .Matches("[^a-zA-Z0-9]").WithMessage("Yeni şifre en az bir özel karakter içermelidir.");

            // ConfirmNewPassword (Yeni Şifre Tekrarı) alanı için doğrulama kuralları.
            RuleFor(x => x.ConfirmNewPassword)
                // Yeni şifre tekrarının boş olmamasını sağlar.
                .NotEmpty().WithMessage("Yeni şifre tekrarı boş olamaz.")
                // Yeni şifre tekrarının, NewPassword alanı ile aynı olmasını sağlar.
                .Equal(x => x.NewPassword).WithMessage("Şifreler uyuşmuyor.");
        }
    }
} 