using MediatR;
using System;

namespace DonationTrackingSystem.Application.Features.Kullanici.Commands
{
    /// <summary>
    /// Kullanıcı bilgilerini güncelleme işlemini temsil eden MediatR komut sınıfı.
    /// </summary>
    public class UpdateUserCommand : IRequest<bool>
    {
        /// <summary>
        /// Güncellenecek kullanıcının benzersiz kimlik numarası.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Kullanıcının güncellenmiş adı ve soyadı.
        /// </summary>
        public required string AdSoyad { get; set; }

        /// <summary>
        /// Kullanıcının güncellenmiş e-posta adresi.
        /// </summary>
        public required string Eposta { get; set; }

        /// <summary>
        /// Kullanıcının güncellenmiş rolü.
        /// </summary>
        public required DonationTrackingSystem.Domain.Enums.Rol Rol { get; set; }
    }
}
