namespace DonationTrackingSystem.Application.DTOs
{
    /// <summary>
    /// Kullanıcı bilgilerini güncellemek için kullanılan veri transfer nesnesi.
    /// </summary>
    public class UserUpdateDto
    {
        /// <summary>
        /// Kullanıcının güncellenmiş adı ve soyadı.
        /// </summary>
        public required string AdSoyad { get; set; }

        /// <summary>
        /// Kullanıcının güncellenmiş e-posta adresi.
        /// </summary>
        public required string Eposta { get; set; }

        /// <summary>
        /// Kullanıcının güncellenmiş rolü.
        /// </summary>
        public DonationTrackingSystem.Domain.Enums.Rol Rol { get; set; }
    }
}
