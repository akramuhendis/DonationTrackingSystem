using MediatR;
using DonationTrackingSystem.Application.DTOs;
using System.Collections.Generic;

namespace DonationTrackingSystem.Application.Features.Kullanici.Queries
{
    /// <summary>
    /// Tüm kullanıcıların listesini sorgulamak için kullanılan MediatR sorgu sınıfı.
    /// Bu sorgu, CQRS (Command Query Responsibility Segregation) tasarım deseninin 'Query' (Sorgu) tarafını oluşturur.
    /// Herhangi bir parametre almaz ve tüm kayıtlı kullanıcıların detaylarını (KullaniciDto listesi) döndürmeyi hedefler.
    /// </summary>
    public class GetUserListQuery : IRequest<List<KullaniciDto>>
    {
        /// <summary>
        /// Getirilecek sayfa numarası. Varsayılan değer 1'dir.
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// Sayfa başına düşecek kayıt sayısı. Varsayılan değer 10'dur.
        /// </summary>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// Kullanıcıları filtrelemek için e-posta adresi.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Kullanıcıları filtrelemek için rol.
        /// </summary>
        public DonationTrackingSystem.Domain.Enums.Rol? Rol { get; set; }
    }
}