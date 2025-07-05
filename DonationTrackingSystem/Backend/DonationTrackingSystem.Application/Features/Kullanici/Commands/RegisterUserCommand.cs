using MediatR;
using DonationTrackingSystem.Application.DTOs;

namespace DonationTrackingSystem.Application.Features.Kullanici.Commands
{
    /// <summary>
    /// Yeni kullanıcı kaydı işlemini temsil eden MediatR komut sınıfı.
    /// Bu komut, CQRS (Command Query Responsibility Segregation) tasarım deseninin 'Command' (Komut) tarafını oluşturur.
    /// Bir kullanıcının sisteme yeni bir hesap oluşturma isteğini taşır.
    /// Bu komut, ilgili 'RegisterUserCommandHandler' tarafından işlenerek kullanıcı bilgilerinin doğrulanması,
    /// şifrenin hash'lenmesi ve kullanıcının veritabanına kaydedilmesi gibi süreçleri tetikler.
    /// Başarılı bir kayıt işleminin ardından, istemciye geri döndürülecek bir onay mesajı veya yeni kullanıcının ID'si gibi bir değer üretilmesini sağlar.
    /// </summary>
    public class RegisterUserCommand : IRequest<Guid> // IRequest<string> arayüzü, bu komutun işlendikten sonra bir 'string' (örneğin, başarı mesajı veya yeni kullanıcı ID'si) tipinde bir sonuç döndüreceğini belirtir.
    {
        /// <summary>
        /// Kullanıcı kayıt bilgilerini içeren DTO.
        /// </summary>
        public required UserRegisterDto UserRegister { get; set; }
    }
}