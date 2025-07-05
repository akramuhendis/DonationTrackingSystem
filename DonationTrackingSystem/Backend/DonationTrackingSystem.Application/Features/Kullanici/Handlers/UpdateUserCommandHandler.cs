using MediatR;
using AutoMapper;
using DonationTrackingSystem.Domain.Exceptions;
using DonationTrackingSystem.Domain.Repositories;
using DonationTrackingSystem.Domain.Entities;
using DonationTrackingSystem.Application.Features.Kullanici.Commands;

namespace DonationTrackingSystem.Application.Features.Kullanici.Handlers
{
    /// <summary>
    /// Kullanıcı güncelleme komutunu (UpdateUserCommand) işleyen MediatR işleyicisidir.
    /// </summary>
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var kullanici = await _unitOfWork.KullaniciRepository.GetByIdAsync(request.Id);
            if (kullanici == null)
            {
                throw new DomainException("Kullanıcı bulunamadı.");
            }

            // E-posta benzersizliğini kontrol et (eğer e-posta değişmişse)
            if (kullanici.Eposta != request.Eposta)
            {
                var existingUserWithNewEmail = await _unitOfWork.KullaniciRepository.GetByEmailAsync(request.Eposta);
                if (existingUserWithNewEmail != null && existingUserWithNewEmail.Id != request.Id)
                {
                    throw new DomainException("Bu e-posta adresi başka bir kullanıcı tarafından kullanılıyor.");
                }
            }

            // AutoMapper ile gelen verileri mevcut kullanıcı nesnesine eşle
            // Not: SifreHash gibi hassas alanların doğrudan DTO'dan eşlenmediğinden emin olun.
            _mapper.Map(request, kullanici);

            // Rol ataması (UpdateUserCommand'daki Rol zaten enum olduğu için doğrudan atanabilir)
            // İdealde, bu işlem için ayrı bir yetkilendirme servisi kullanılmalı veya
            // sadece belirli rollerin atanmasına izin verilmelidir.
            // Şu anki durumda, validator zaten geçerli bir rol olmasını sağlıyor, ancak yetkilendirme kontrolü eksik.
            // kullanici.Rol = (Domain.Enums.Rol)Enum.Parse(typeof(Domain.Enums.Rol), request.Rol, true); // Bu satır artık gerekli değil

            await _unitOfWork.KullaniciRepository.UpdateAsync(kullanici);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
