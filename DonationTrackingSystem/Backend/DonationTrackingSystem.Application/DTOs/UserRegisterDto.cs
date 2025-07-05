namespace DonationTrackingSystem.Application.DTOs
{
    /// <summary>
    /// Yeni kullanıcı kaydı için kullanılan veri transfer nesnesi.
    /// </summary>
    public class UserRegisterDto
    {
        /// <summary>
        /// Yeni kullanıcının tam adı veya kullanıcı adı.
        /// </summary>
        public required string AdSoyad { get; set; }

        /// <summary>
        /// Yeni kullanıcının sisteme kayıt olmak için kullanacağı e-posta adresi.
        /// </summary>
        public required string Email { get; set; }

        /// <summary>
        /// Yeni kullanıcının sisteme giriş yapmak için belirleyeceği şifre.
        /// </summary>
        public required string Sifre { get; set; }

        /// <summary>
        /// Yeni kullanıcının sistemdeki rolü.
        /// </summary>
        public DonationTrackingSystem.Domain.Enums.Rol Rol { get; set; }
    }
}