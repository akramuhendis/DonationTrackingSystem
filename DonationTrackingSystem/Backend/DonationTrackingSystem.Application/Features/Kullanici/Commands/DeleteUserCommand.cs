using MediatR;
using System;

namespace DonationTrackingSystem.Application.Features.Kullanici.Commands
{
    /// <summary>
    /// Kullanıcı silme işlemini temsil eden MediatR komut sınıfı.
    /// </summary>
    public class DeleteUserCommand : IRequest<bool>
    {
        /// <summary>
        /// Silinecek kullanıcının benzersiz kimlik numarası.
        /// </summary>
        public Guid Id { get; set; }
    }
}
