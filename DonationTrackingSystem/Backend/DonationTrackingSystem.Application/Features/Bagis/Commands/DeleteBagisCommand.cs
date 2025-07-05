using MediatR;

namespace DonationTrackingSystem.Application.Features.Bagis.Commands
{
    // DeleteBagisCommand, belirli bir bağışı silmek için kullanılan MediatR komutudur.
    // Bu komut, CQRS deseninde komut katmanında kullanılır ve handler tarafından işlenir.
    // IRequest<bool> ile işleyici tarafından silme işleminin başarılı olup olmadığı bool olarak döndürülür.
    public class DeleteBagisCommand : IRequest<bool>
    {
        // Silinecek bağışın benzersiz kimliği (Guid tipinde olmalı)
        // Bu Id, silinecek bağışın veritabanındaki anahtarını temsil eder.
        public Guid Id { get; set; }
    }
}