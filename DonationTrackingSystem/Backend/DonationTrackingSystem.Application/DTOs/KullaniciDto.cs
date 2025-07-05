using System;

namespace DonationTrackingSystem.Application.DTOs
{
    // KullaniciDto, kullanıcı bilgilerini taşımak için kullanılan bir Data Transfer Object (DTO) sınıfıdır.
    public class KullaniciDto
    {
        // Kullanıcının benzersiz kimliği
        public Guid Id { get; set; }
        // Kullanıcının adı ve soyadı. Nullable olarak işaretlendi.
        public string? AdSoyad { get; set; }
        // Kullanıcının e-posta adresi. Nullable olarak işaretlendi.
        public string? Email { get; set; }
        // Kullanıcının rolü. Nullable olarak işaretlendi.
        public DonationTrackingSystem.Domain.Enums.Rol? Rol { get; set; }
        
    }
}