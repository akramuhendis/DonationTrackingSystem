using MediatR;
using DonationTrackingSystem.Application.DTOs;

namespace DonationTrackingSystem.Application.Features.Kullanici.Queries
{
    /// <summary>
    /// Belirli bir kullanıcının detaylarını sorgulamak için kullanılan MediatR sorgu sınıfı.
    /// Bu sorgu, CQRS (Command Query Responsibility Segregation) tasarım deseninin 'Query' (Sorgu) tarafını oluşturur.
    /// Kullanıcının benzersiz kimliği (ID) ile detaylı bilgilerini (KullaniciDto) almak için kullanılır.
    /// </summary>
    public class GetUserDetailQuery : IRequest<KullaniciDto>
    {
        /// <summary>
        /// Detayları sorgulanacak kullanıcının benzersiz kimliği (ID).
        /// Bu alan, sorgunun hangi kullanıcıya ait bilgileri getireceğini belirler.
        /// 'required' anahtar kelimesi, bu özelliğin 'GetUserDetailQuery' nesnesi oluşturulurken mutlaka bir değer alması gerektiğini belirtir.
        /// </summary>
        public required System.Guid KullaniciId { get; set; }
    }
}