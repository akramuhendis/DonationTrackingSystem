using MediatR;
using DonationTrackingSystem.Application.Features.Kullanici.Commands;
using DonationTrackingSystem.Domain.Repositories;
using DonationTrackingSystem.Domain.Exceptions;
using DonationTrackingSystem.Domain.Services; // IJwtService ve IPasswordHasher'ın burada olduğunu varsayıyorum
using System.Threading;
using System.Threading.Tasks;
using System.Linq; // FirstOrDefault için gerekli

namespace DonationTrackingSystem.Application.Features.Kullanici.Handlers
{
    /// <summary>
    /// Kullanıcı girişi komutunu işleyen MediatR işleyici sınıfı.
    /// Bu sınıf, 'LoginUserCommand' komutunu alır ve kullanıcının kimlik bilgilerini doğrulayarak bir JWT (JSON Web Token) döndürür.
    /// CQRS (Command Query Responsibility Segregation) tasarım deseninin 'Command' (Komut) tarafını işler.
    /// </summary>
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork; // Unit of Work, veritabanı işlemlerini tek bir transaction altında toplamak için kullanılır.
        private readonly IPasswordHasher _passwordHasher; // Şifre doğrulama ve hashleme için servis
        private readonly IJwtService _jwtService; // JWT token oluşturma için servis

        /// <summary>
        /// LoginUserCommandHandler sınıfının yapıcı metodu.
        /// Gerekli bağımlılıkları (IUnitOfWork, IPasswordHasher, IJwtService) enjekte eder.
        /// </summary>
        /// <param name="unitOfWork">Veritabanı işlemleri için Unit of Work arayüzü.</param>
        /// <param name="passwordHasher">Şifre doğrulama ve hashleme servisi.</param>
        /// <param name="jwtService">JWT token oluşturma servisi.</param>
        public LoginUserCommandHandler(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, IJwtService jwtService)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
        }

        /// <summary>
        /// LoginUserCommand komutunu işler ve başarılı giriş durumunda bir JWT token döndürür.
        /// </summary>
        /// <param name="request">İşlenecek LoginUserCommand komutu.</param>
        /// <param name="cancellationToken">İptal token'ı.</param>
        /// <returns>Başarılı giriş durumunda JWT token.</returns>
        /// <exception cref="DomainException">Kullanıcı bulunamazsa veya şifre yanlışsa fırlatılır.</exception>
        public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            // Kullanıcıyı e-posta adresine göre veritabanından getir
            var kullanici = await _unitOfWork.KullaniciRepository.GetByEmailAsync(request.UserLogin.Email);

            // Kullanıcı bulunamazsa hata fırlat
            if (kullanici == null)
            {
                throw new DomainException("Geçersiz e-posta veya şifre.");
            }

            // Şifreyi doğrula
            if (!_passwordHasher.VerifyPassword(request.UserLogin.Sifre, kullanici.SifreHash))
            {
                throw new DomainException("Geçersiz e-posta veya şifre.");
            }

            // JWT token oluştur
            var token = _jwtService.GenerateToken(kullanici.Id.ToString(), kullanici.Eposta, kullanici.Rol.ToString());

            return token;
        }
    }
}