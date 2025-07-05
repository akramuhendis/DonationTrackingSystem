using MediatR; // MediatR kütüphanesi, CQRS (Command Query Responsibility Segregation) desenini uygulamak için kullanılır. Komutların ve sorguların işlenmesini sağlar.
using DonationTrackingSystem.Domain.Repositories; // Domain katmanındaki repository arayüzlerine erişim sağlar. Veritabanı işlemleri için IKullaniciRepository kullanılır.
using DonationTrackingSystem.Application.Features.Kullanici.Commands; // Kullanıcı ile ilgili komut tanımlarına erişim sağlar. ResetPasswordCommand burada işlenir.
using DonationTrackingSystem.Domain.Exceptions;
using DonationTrackingSystem.Domain.Services; // Domain katmanındaki servis arayüzlerine erişim sağlar. Özellikle IPasswordHasher kullanılır.

namespace DonationTrackingSystem.Application.Features.Kullanici.Handlers
{
    /// <summary>
    /// Kullanıcı şifre sıfırlama komutunu (ResetPasswordCommand) işleyen MediatR işleyicisidir.
    /// IRequestHandler arayüzünü uygulayarak, belirli bir komut (ResetPasswordCommand) için belirli bir yanıt (bool) döndürme yeteneği sağlar.
    /// Bu sınıf, gelen şifre sıfırlama isteğini alır, iş mantığını uygular (kullanıcıyı bulma, şifreyi hash'leme, veritabanında güncelleme) ve işlemin sonucunu döndürür.
    /// </summary>
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, bool>
    {
        // IKullaniciRepository arayüzü, kullanıcı veritabanı işlemlerini soyutlar.
        // Dependency Injection (Bağımlılık Enjeksiyonu) prensibiyle dışarıdan sağlanır.
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenService _tokenService;

        public ResetPasswordCommandHandler(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
        }

        /// <summary>
        /// ResetPasswordCommand komutunu asenkron olarak işleyen metodudur.
        /// MediatR kütüphanesi tarafından çağrılır ve komutun ana iş mantığını içerir.
        /// </summary>
        /// <param name="request">İşlenecek olan ResetPasswordCommand nesnesi. Şifre sıfırlama bilgilerini içerir.</param>
        /// <param name="cancellationToken">Asenkron işlemlerde iptal sinyali için kullanılır.</param>
        /// <returns>Şifre sıfırlama işleminin başarılı olup olmadığını belirten bir boolean değer döner.</returns>
        public async Task<bool> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            // E-posta adresine göre kullanıcıyı asenkron olarak veritabanından getirir.
            // Eğer kullanıcı bulunamazsa, şifre sıfırlama işlemi başarısız kabul edilir.
            var kullanici = await _unitOfWork.KullaniciRepository.GetByEmailAsync(request.Email);
            if (kullanici == null)
            {
                return false;
            }

            // ResetToken doğrulaması
            if (!await _tokenService.ValidateResetTokenAsync(request.Email, request.ResetToken))
            {
                throw new DomainException("Geçersiz veya süresi dolmuş sıfırlama token'ı.");
            }

            // Yeni şifrelerin eşleştiğini kontrol et (Validatörde de kontrol ediliyor, burada ek bir güvenlik katmanı)
            if (request.NewPassword != request.ConfirmNewPassword)
            {
                throw new DomainException("Yeni şifreler uyuşmuyor.");
            }

            // Yeni şifreyi hashle ve güncelle
            kullanici.SifreHash = _passwordHasher.HashPassword(request.NewPassword);
            await _unitOfWork.KullaniciRepository.UpdateAsync(kullanici);
            await _unitOfWork.SaveChangesAsync();

            // Token'ı geçersiz kıl (tek kullanımlık olması için)
            await _tokenService.InvalidateResetTokenAsync(request.ResetToken);

            return true;
        }
    }
} 