namespace DonationTrackingSystem.Application.DTOs
{
    /// <summary>
    /// Kullanıcı girişi için kullanılan veri transfer nesnesi.
    /// </summary>
    public class UserLoginDto
    {
        /// <summary>
        /// Kullanıcının e-posta adresi.
        /// </summary>
        public required string Email { get; set; }

        /// <summary>
        /// Kullanıcının şifresi.
        /// </summary>
        public required string Sifre { get; set; }
    }
}