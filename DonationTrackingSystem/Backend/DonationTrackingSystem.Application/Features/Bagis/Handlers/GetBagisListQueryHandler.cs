using AutoMapper;
using MediatR;
using DonationTrackingSystem.Domain.Repositories;
using DonationTrackingSystem.Application.DTOs;
using DonationTrackingSystem.Application.Features.Bagis.Queries;

namespace DonationTrackingSystem.Application.Features.Bagis.Handlers
{
    // XML Yorumları: Bu yorumlar, /// ile başlar ve kodun ne yaptığını, parametrelerin ne anlama geldiğini
    // ve ne döndürdüğünü açıklamak için kullanılır. Bu, IntelliSense tarafından okunarak geliştiriciye anlık bilgi sağlar.
    
    /// <summary>
    /// `GetBagisListQuery` tipindeki sorguları işlemekle sorumlu olan sınıftır.
    /// Bu sınıf, CQRS (Command Query Responsibility Segregation) mimari deseninin bir parçasıdır.
    /// Görevi, sadece veri okuma (Query) operasyonlarını yönetmektir.
    /// </summary>
    public class GetBagisListQueryHandler : IRequestHandler<GetBagisListQuery, List<BagisDto>>
    {
        // Dependency Injection (Bağımlılık Enjeksiyonu):
        // Bu sınıf, doğrudan veritabanı veya AutoMapper'ın somut sınıflarına bağımlı değildir.
        // Bunun yerine, arayüzler (interface) üzerinden çalışır. Bu, "Dependency Inversion Principle" (SOLID'in D'si) ilkesine uygundur.
        // Bu sayede, bu handler'ı test etmek için sahte (mock) repository ve mapper nesneleri kolayca kullanılabilir.
        
        /// <summary>
        /// Veritabanı işlemlerini soyutlayan repository arayüzü.
        /// Bu handler, veritabanına nasıl erişildiğini bilmek zorunda değildir, sadece arayüzdeki metotları çağırır.
        /// </summary>
        private readonly IBagisRepository _bagisRepository;

        /// <summary>
        /// Nesneler arası haritalama (mapping) işlemlerini yapan AutoMapper arayüzü.
        /// `Bagis` (Domain Entity) nesnesini `BagisDto` (Data Transfer Object) nesnesine dönüştürmek için kullanılır.
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Handler'ın yapıcı metodu (constructor).
        /// Gerekli olan bağımlılıklar (IBagisRepository, IMapper) bu metot aracılığıyla DI (Dependency Injection) konteyneri tarafından sağlanır.
        /// </summary>
        /// <param name="bagisRepository">Bağış veritabanı işlemleri için somut repository nesnesi.</param>
        /// <param name="mapper">AutoMapper'ın somut nesnesi.</param>
        public GetBagisListQueryHandler(IBagisRepository bagisRepository, IMapper mapper)
        {
            _bagisRepository = bagisRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// MediatR kütüphanesi tarafından `GetBagisListQuery` gönderildiğinde otomatik olarak çağrılan metot.
        /// Sorgunun asıl iş mantığı burada yer alır.
        /// </summary>
        /// <param name="request">İstemciden gelen ve sayfalama bilgilerini (`PageNumber`, `PageSize`) içeren sorgu nesnesi.</param>
        /// <param name="cancellationToken">İsteğin iptal edilip edilmediğini kontrol etmek için kullanılan bir token.</param>
        /// <returns>İşlem sonucunda `BagisDto` tipinde bir liste döndürür.</returns>
        public async Task<List<BagisDto>> Handle(GetBagisListQuery request, CancellationToken cancellationToken)
        {
            // Adım 1: Veri Çekme (Data Retrieval)
            // Repository katmanı aracılığıyla veritabanından veriler çekilir.
            // `GetAllAsync` yerine `GetPagedAsync` kullanmak, veritabanından tüm kayıtları çekmek yerine sadece
            // istenen sayfadaki kadar veriyi çekerek performansı ciddi ölçüde artırır. Bu, "best practice" olarak kabul edilir.
            var (bagislar, totalCount) = await _bagisRepository.GetPagedAsync(
                request.PageNumber, 
                request.PageSize, 
                cancellationToken: cancellationToken);
            
            // Not: `totalCount` şu anda kullanılmıyor, ancak gelecekte API yanıtına toplam kayıt sayısını
            // eklemek için (örneğin, sayfalama bilgisini UI'a göndermek için) kullanılabilir.

            // Adım 2: Haritalama (Mapping)
            // Veritabanından gelen `Bagis` entity listesi, API'nin dış dünyaya sunacağı `BagisDto` listesine dönüştürülür.
            // Bu ayrım, domain modelinizin (iç mantığınızın) dış dünya bağımlılıklarından (API kontratları) izole kalmasını sağlar.
            return _mapper.Map<List<BagisDto>>(bagislar);
        }
    }
}