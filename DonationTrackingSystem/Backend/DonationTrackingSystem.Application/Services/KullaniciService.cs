using MediatR;
using DonationTrackingSystem.Application.DTOs;
using DonationTrackingSystem.Application.Interfaces;
using DonationTrackingSystem.Application.Features.Kullanici.Commands;
using DonationTrackingSystem.Application.Features.Kullanici.Queries;
using AutoMapper;

namespace DonationTrackingSystem.Application.Services
{
    /// <summary>
    /// Kullanıcı yönetimi işlemleri için uygulama servisinin implementasyonudur.
    /// MediatR komut ve sorgularını kullanarak iş mantığını yürütür.
    /// </summary>
    public class KullaniciService : IKullaniciService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        /// <summary>
        /// KullaniciService sınıfının yapıcı metodu.
        /// </summary>
        /// <param name="mediator">MediatR arayüzü.</param>
        /// <param name="mapper">AutoMapper arayüzü.</param>
        public KullaniciService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Yeni bir kullanıcıyı sisteme kaydeder.
        /// </summary>
        /// <param name="userRegisterDto">Kaydedilecek kullanıcının bilgilerini içeren DTO.</param>
        /// <returns>Oluşturulan yeni kullanıcının ID'si.</returns>
        public async Task<Guid> RegisterUserAsync(UserRegisterDto userRegisterDto)
        {
            var command = _mapper.Map<RegisterUserCommand>(userRegisterDto);
            return await _mediator.Send(command);
        }

        /// <summary>
        /// Kullanıcının e-posta ve şifresini kullanarak sisteme giriş yapmasını sağlar.
        /// </summary>
        /// <param name="email">Kullanıcının e-posta adresi.</param>
        /// <param name="sifre">Kullanıcının şifresi.</param>
        /// <returns>Başarılı girişte bir JWT (JSON Web Token) veya kimlik doğrulama anahtarı.</returns>
        public async Task<string?> LoginUserAsync(string email, string sifre)
        {
            var command = new LoginUserCommand { UserLogin = new UserLoginDto { Email = email, Sifre = sifre } };
            return await _mediator.Send(command);
        }

        /// <summary>
        /// Kullanıcının şifresini sıfırlar.
        /// </summary>
        /// <param name="email">Şifresi sıfırlanacak kullanıcının e-posta adresi.</param>
        /// <param name="resetToken">Şifre sıfırlama token'ı.</param>
        /// <param name="yeniSifre">Kullanıcı için ayarlanacak yeni şifre.</param>
        /// <returns>İşlemin başarılı olup olmadığını belirten boolean değeri.</returns>
        public async Task<bool> ResetPasswordAsync(string email, string resetToken, string yeniSifre)
        {
            var command = new ResetPasswordCommand { Email = email, ResetToken = resetToken, NewPassword = yeniSifre, ConfirmNewPassword = yeniSifre };
            return await _mediator.Send(command);
        }

        /// <summary>
        /// Tüm kullanıcıların listesini getirir.
        /// </summary>
        /// <returns>Kullanıcı DTO'larından oluşan liste.</returns>
        public async Task<List<KullaniciDto>> GetUserListAsync()
        {
            var query = new GetUserListQuery();
            return await _mediator.Send(query);
        }

        /// <summary>
        /// Belirli bir kullanıcının detaylarını getirir.
        /// </summary>
        /// <param name="id">Kullanıcı ID'si.</param>
        /// <returns>Kullanıcı DTO'su veya null.</returns>
        public async Task<KullaniciDto?> GetUserDetailAsync(Guid id)
        {
            var query = new GetUserDetailQuery { KullaniciId = id };
            return await _mediator.Send(query);
        }

        /// <summary>
        /// Belirli bir kullanıcının bilgilerini günceller.
        /// </summary>
        /// <param name="id">Güncellenecek kullanıcının ID'si.</param>
        /// <param name="userUpdateDto">Güncel kullanıcı bilgilerini içeren DTO.</param>
        /// <returns>Güncelleme işleminin başarılı olup olmadığını belirten boolean değeri.</returns>
        public async Task<bool> UpdateUserAsync(Guid id, UserUpdateDto userUpdateDto)
        {
            var command = _mapper.Map<UpdateUserCommand>(userUpdateDto);
            command.Id = id;
            return await _mediator.Send(command);
        }

        /// <summary>
        /// Belirli bir kullanıcıyı siler.
        /// </summary>
        /// <param name="id">Silinecek kullanıcının ID'si.</param>
        /// <returns>Silme işleminin başarılı olup olmadığını belirten boolean değeri.</returns>
        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var command = new DeleteUserCommand { Id = id };
            return await _mediator.Send(command);
        }

        /// <summary>
        /// Mevcut şifresini bilen bir kullanıcının şifresini değiştirir.
        /// </summary>
        /// <param name="userId">Şifresini değiştirecek kullanıcının ID'si.</param>
        /// <param name="oldPassword">Kullanıcının mevcut (eski) şifresi.</param>
        /// <param name="newPassword">Kullanıcının ayarlamak istediği yeni şifre.</param>
        /// <returns>Şifre değiştirme işleminin başarılı olup olmadığını belirten boolean değeri.</returns>
        public async Task<bool> ChangePasswordAsync(Guid userId, string oldPassword, string newPassword)
        {
            var command = new ChangePasswordCommand { UserId = userId, CurrentPassword = oldPassword, NewPassword = newPassword, ConfirmNewPassword = newPassword };
            return await _mediator.Send(command);
        }
    }
}
