using MediatR;
using DonationTrackingSystem.Application.DTOs;

namespace DonationTrackingSystem.Application.Features.Kullanici.Commands
{
    /// <summary>
    /// Kullanıcı girişi işlemini temsil eden MediatR komut sınıfı.
    /// Bu komut, CQRS (Command Query Responsibility Segregation) tasarım deseninin 'Command' (Komut) tarafını oluşturur.
    /// Bir kullanıcının sisteme e-posta adresi ve şifresi ile güvenli bir şekilde giriş yapma isteğini taşır.
    /// Bu komut, ilgili 'LoginUserCommandHandler' tarafından işlenerek kimlik doğrulama ve yetkilendirme süreçlerini tetikler.
    /// Başarılı bir kimlik doğrulamasının ardından, istemciye geri döndürülecek bir JWT (JSON Web Token) üretilmesini sağlar.
    /// </summary>
    public class LoginUserCommand : IRequest<string> // IRequest<string> arayüzü, MediatR kütüphanesinin bir parçasıdır ve bu komutun işlendikten sonra bir 'string' (bu durumda JWT Token) tipinde bir sonuç döndüreceğini belirtir.
    {
        /// <summary>
        /// Kullanıcı giriş bilgilerini içeren DTO.
        /// </summary>
        public required UserLoginDto UserLogin { get; set; }
    }
} 