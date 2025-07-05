using MediatR;
using DonationTrackingSystem.Domain.Repositories;
using DonationTrackingSystem.Domain.Exceptions;
using DonationTrackingSystem.Domain.Services;
using DonationTrackingSystem.Application.Features.Kullanici.Commands;

namespace DonationTrackingSystem.Application.Features.Kullanici.Handlers
{
    /// <summary>
    /// Kullanıcı şifre değiştirme komutunu (ChangePasswordCommand) işleyen MediatR işleyicisidir.
    /// </summary>
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;

        public ChangePasswordCommandHandler(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
        }

        public async Task<bool> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var kullanici = await _unitOfWork.KullaniciRepository.GetByIdAsync(request.UserId);
            if (kullanici == null)
            {
                throw new DomainException("Kullanıcı bulunamadı.");
            }

            // Mevcut şifreyi doğrula
            if (!_passwordHasher.VerifyPassword(request.CurrentPassword, kullanici.SifreHash))
            {
                throw new DomainException("Mevcut şifre yanlış.");
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

            return true;
        }
    }
}
