using FluentValidation;
using DonationTrackingSystem.Application.Features.Kullanici.Commands;

namespace DonationTrackingSystem.Application.Validators
{
    /// <summary>
    /// ChangePasswordCommand için doğrulama kurallarını tanımlar.
    /// </summary>
    public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("Kullanıcı ID'si boş olamaz.");

            RuleFor(x => x.CurrentPassword)
                .NotEmpty().WithMessage("Mevcut şifre boş olamaz.");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("Yeni şifre boş olamaz.")
                .MinimumLength(8).WithMessage("Yeni şifre en az 8 karakter olmalıdır.")
                .Matches("[A-Z]").WithMessage("Yeni şifre en az bir büyük harf içermelidir.")
                .Matches("[a-z]").WithMessage("Yeni şifre en az bir küçük harf içermelidir.")
                .Matches("[0-9]").WithMessage("Yeni şifre en az bir rakam içermelidir.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Yeni şifre en az bir özel karakter içermelidir.");

            RuleFor(x => x.ConfirmNewPassword)
                .NotEmpty().WithMessage("Yeni şifre tekrarı boş olamaz.")
                .Equal(x => x.NewPassword).WithMessage("Şifreler uyuşmuyor.");
        }
    }
}