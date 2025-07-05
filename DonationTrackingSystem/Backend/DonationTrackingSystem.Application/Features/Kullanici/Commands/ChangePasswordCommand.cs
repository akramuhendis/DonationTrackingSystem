using MediatR;
using System;

namespace DonationTrackingSystem.Application.Features.Kullanici.Commands
{
    /// <summary>
    /// Kullanıcı şifresini değiştirme işlemini temsil eden MediatR komut sınıfı.
    /// </summary>
    public class ChangePasswordCommand : IRequest<bool>
    {
        /// <summary>
        /// Şifresi değiştirilecek kullanıcının benzersiz kimlik numarası.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Kullanıcının mevcut şifresi.
        /// </summary>
        public required string CurrentPassword { get; set; }

        /// <summary>
        /// Kullanıcının yeni şifresi.
        /// </summary>
        public required string NewPassword { get; set; }

        /// <summary>
        /// Yeni şifrenin tekrarı.
        /// </summary>
        public required string ConfirmNewPassword { get; set; }
    }
}
