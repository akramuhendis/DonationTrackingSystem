using MediatR;
using System;

namespace DonationTrackingSystem.Application.Features.Bagis.Commands
{
    // UpdateBagisCommand, mevcut bir bağış kaydını güncellemek için kullanılan MediatR komutudur.
    // Bu komut, CQRS deseninde komut katmanında kullanılır ve handler tarafından işlenir.
    // IRequest<bool> ile işleyici tarafından güncelleme işleminin başarılı olup olmadığı bool olarak döndürülür.
    public class UpdateBagisCommand : IRequest<bool>
    {
        // Güncellenecek bağışın benzersiz kimliği (Guid tipinde olmalı)
        // Bu Id, güncellenecek bağışın veritabanındaki anahtarını temsil eder.
        public Guid Id { get; set; }
        // Bağışı yapan kişinin adı. Nullable olarak işaretlendi.
        // Kullanıcıdan alınan ad-soyad bilgisini temsil eder.
        public string? BagisciAdi { get; set; }
        // Bağış miktarı. Zorunlu bir alandır.
        // Kullanıcıdan alınan bağış tutarını temsil eder.
        public decimal Tutar { get; set; }
        // Bağış türü. Nullable olarak işaretlendi.
        // Örneğin: "Nakit", "Eşya", "Hizmet" gibi türler olabilir.
        public DonationTrackingSystem.Domain.Enums.BagisTuru? BagisTuru { get; set; }
        // Bağış tarihi. Nullable olarak işaretlendi.
        // Kullanıcı tarafından girilmezse, sistem tarafından bugünün tarihi atanabilir.
        public DateTime? Tarih { get; set; }
        // Bağış açıklaması. Nullable olarak işaretlendi.
        // Bağış ile ilgili ek bilgi veya notlar için kullanılır.
        public string? Aciklama { get; set; }
    }
}