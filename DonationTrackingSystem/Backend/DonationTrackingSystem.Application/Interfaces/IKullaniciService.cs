using DonationTrackingSystem.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DonationTrackingSystem.Application.Interfaces
{
    /// <summary>
    /// Kullanıcı yönetimi işlemleri için uygulama servis arayüzü.
    /// Bu arayüz, kullanıcı kaydı, kimlik doğrulama ve diğer kullanıcı yönetimi operasyonlarını tanımlar.
    /// </summary>
    public interface IKullaniciService
    {
        /// <summary>
        /// Yeni bir kullanıcıyı sisteme asenkron olarak kaydeder.
        /// </summary>
        /// <param name="kullaniciDto">Kaydedilecek kullanıcının bilgilerini içeren DTO.</param>
        /// <returns>Oluşturulan yeni kullanıcının ID'sini içeren bir Task.</returns>
        Task<Guid> RegisterUserAsync(UserRegisterDto userRegisterDto);

        /// <summary>
        /// Kullanıcının e-posta ve şifresini kullanarak sisteme giriş yapmasını sağlar.
        /// </summary>
        /// <param name="email">Kullanıcının e-posta adresi.</param>
        /// <param name="sifre">Kullanıcının şifresi.</param>
        /// <returns>Başarılı girişte bir JWT (JSON Web Token) veya kimlik doğrulama anahtarı içeren bir Task. Başarısız olursa null döner.</returns>
        Task<string?> LoginUserAsync(string email, string sifre);

        /// <summary>
        /// Kullanıcının şifresini asenkron olarak sıfırlar.
        /// Genellikle "şifremi unuttum" senaryoları için kullanılır.
        /// </summary>
        /// <param name="email">Şifresi sıfırlanacak kullanıcının e-posta adresi.</param>
        /// <param name="yeniSifre">Kullanıcı için ayarlanacak yeni şifre.</param>
        /// <returns>İşlemin başarılı olup olmadığını belirten bir boolean değeri içeren Task.</returns>
        Task<bool> ResetPasswordAsync(string email, string resetToken, string yeniSifre);

        /// <summary>
        /// Sistemdeki tüm kullanıcıların bir listesini asenkron olarak getirir.
        /// </summary>
        /// <returns>Kullanıcı DTO'larından oluşan bir listeyi içeren bir Task.</returns>
        Task<List<KullaniciDto>> GetUserListAsync();

        /// <summary>
        /// Belirtilen ID'ye sahip kullanıcının detaylarını asenkron olarak getirir.
        /// </summary>
        /// <param name="id">Detayları getirilecek kullanıcının ID'si.</param>
        /// <returns>Bulunan kullanıcının DTO'sunu veya bulunamazsa null içeren bir Task.</returns>
        Task<KullaniciDto?> GetUserDetailAsync(Guid id);

        /// <summary>
        /// Belirtilen ID'ye sahip kullanıcının bilgilerini asenkron olarak günceller.
        /// </summary>
        /// <param name="id">Güncellenecek kullanıcının ID'si.</param>
        /// <param name="kullaniciDto">Güncel kullanıcı bilgilerini içeren DTO.</param>
        /// <returns>Operasyonun tamamlandığını belirten bir Task.</returns>
        Task<bool> UpdateUserAsync(Guid id, UserUpdateDto userUpdateDto);

        /// <summary>
        /// Belirtilen ID'ye sahip kullanıcıyı asenkron olarak siler.
        /// </summary>
        /// <param name="id">Silinecek kullanıcının ID'si.</param>
        /// <returns>Operasyonun tamamlandığını belirten bir Task.</returns>
        Task<bool> DeleteUserAsync(Guid id);

        /// <summary>
        /// Mevcut şifresini bilen bir kullanıcının şifresini asenkron olarak değiştirmesini sağlar.
        /// </summary>
        /// <param name="userId">Şifresini değiştirecek kullanıcının ID'si.</param>
        /// <param name="oldPassword">Kullanıcının mevcut (eski) şifresi.</param>
        /// <param name="newPassword">Kullanıcının ayarlamak istediği yeni şifre.</param>
        /// <returns>Şifre değiştirme işleminin başarılı olup olmadığını belirten bir boolean değeri içeren Task.</returns>
        Task<bool> ChangePasswordAsync(Guid userId, string oldPassword, string newPassword);
    }
}