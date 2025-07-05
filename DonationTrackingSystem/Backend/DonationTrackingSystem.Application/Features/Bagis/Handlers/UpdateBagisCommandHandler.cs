using AutoMapper;
using MediatR;
using DonationTrackingSystem.Domain.Repositories;
using DonationTrackingSystem.Application.Features.Bagis.Commands;
using System.Threading;
using System.Threading.Tasks;
using DonationTrackingSystem.Domain.Exceptions;

namespace DonationTrackingSystem.Application.Features.Bagis.Handlers
{
    /// <summary>
    /// `UpdateBagisCommand` komutunu işleyen handler sınıfıdır.
    /// Bu sınıf, mevcut bir bağış kaydını güncellemekten sorumludur.
    /// </summary>
    public class UpdateBagisCommandHandler : IRequestHandler<UpdateBagisCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        /// <summary>
        /// Handler'ın yapıcı metodu.
        /// Gerekli olan `IUnitOfWork` ve `IMapper` bağımlılıkları bu metot aracılığıyla enjekte edilir.
        /// </summary>
        /// <param name="unitOfWork">Veritabanı işlemlerini tek bir transaction altında toplamak için kullanılır.</param>
        /// <param name="mapper">Komut nesnesindeki verileri domain entity'sine haritalamak için kullanılır.</param>
        public UpdateBagisCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// `UpdateBagisCommand` komutunu işleyen ana metot.
        /// </summary>
        /// <param name="request">Güncellenecek bağışın ID'sini ve yeni verileri içeren komut nesnesi.</param>
        /// <param name="cancellationToken">İsteğin iptal edilip edilmediğini kontrol eden token.</param>
        /// <returns>Güncelleme işleminin başarılı olup olmadığını belirten bir boolean değer.</returns>
        public async Task<bool> Handle(UpdateBagisCommand request, CancellationToken cancellationToken)
        {
            // Adım 1: Güncellenecek varlığı bul.
            // Komut içerisindeki Id kullanılarak, veritabanından ilgili bağış kaydı bulunur.
            // `GetByIdAsync` metodu, `IBagisRepository` üzerinden çağrılır.
            var bagisToUpdate = await _unitOfWork.BagisRepository.GetByIdAsync(request.Id, cancellationToken: cancellationToken);

            // Adım 2: Varlığın mevcut olup olmadığını kontrol et.
            // Eğer `bagisToUpdate` null ise, bu ID'ye sahip bir kayıt veritabanında bulunamamış demektir.
            // Bu durumda, bir `DomainException` fırlatılarak işlemin devam etmesi engellenir.
            if (bagisToUpdate == null)
            {
                throw new DomainException($"{request.Id} ID'li bağış kaydı bulunamadı.");
            }

            // Adım 3: Verileri haritala (Map).
            // AutoMapper kullanılarak, `request` (UpdateBagisCommand) nesnesindeki veriler,
            // veritabanından çekilen `bagisToUpdate` (Bagis entity) nesnesinin üzerine yazılır.
            // Bu, manuel olarak `bagisToUpdate.Tutar = request.Tutar;` gibi atamalar yapma ihtiyacını ortadan kaldırır.
            _mapper.Map(request, bagisToUpdate);

            // Adım 4: Varlığı güncelle.
            // Repository üzerinden `UpdateAsync` metodu çağrılarak, güncellenmiş `bagisToUpdate` nesnesinin
            // Entity Framework tarafından "Modified" (Değiştirildi) olarak işaretlenmesi sağlanır.
            await _unitOfWork.BagisRepository.UpdateAsync(bagisToUpdate, cancellationToken);

            // Adım 5: Değişiklikleri kaydet.
            // `UnitOfWork` üzerinden `SaveChangesAsync` çağrılarak, yapılan tüm değişiklikler (bu durumda sadece güncelleme)
            // tek bir transaction olarak veritabanına kalıcı olarak yazılır.
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Adım 6: Sonucu döndür.
            // İşlem başarılı bir şekilde tamamlandığı için `true` değeri döndürülür.
            return true;
        }
    }
}
