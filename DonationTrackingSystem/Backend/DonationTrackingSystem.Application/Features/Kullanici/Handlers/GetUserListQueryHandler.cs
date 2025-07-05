using MediatR;
using AutoMapper;
using DonationTrackingSystem.Domain.Repositories;
using DonationTrackingSystem.Application.DTOs;
using DonationTrackingSystem.Application.Features.Kullanici.Queries;
using System.Collections.Generic; // List için gerekli
using System.Threading;
using System.Threading.Tasks;

namespace DonationTrackingSystem.Application.Features.Kullanici.Handlers
{
    /// <summary>
    /// Kullanıcı listesini getirme sorgusunu işleyen MediatR işleyici sınıfı.
    /// Bu sınıf, 'GetUserListQuery' komutunu alır ve tüm kullanıcıların detaylarını 'KullaniciDto' listesi olarak döndürür.
    /// CQRS (Command Query Responsibility Segregation) tasarım deseninin 'Query' (Sorgu) tarafını oluşturur.
    /// </summary>
    public class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, List<KullaniciDto>>
    {
        private readonly IKullaniciRepository _kullaniciRepository; // Kullanıcı verilerine erişim için repository
        private readonly IMapper _mapper; // Nesne dönüşümleri için AutoMapper

        /// <summary>
        /// GetUserListQueryHandler sınıfının yapıcı metodu.
        /// Gerekli bağımlılıkları (IKullaniciRepository ve IMapper) enjekte eder.
        /// </summary>
        /// <param name="kullaniciRepository">Kullanıcı veritabanı işlemleri için repository arayüzü.</param>
        /// <param name="mapper">Nesne dönüşümleri için AutoMapper arayüzü.</param>
        public GetUserListQueryHandler(IKullaniciRepository kullaniciRepository, IMapper mapper)
        {
            _kullaniciRepository = kullaniciRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// GetUserListQuery komutunu işler ve tüm kullanıcıların listesini döndürür.
        /// </summary>
        /// <param name="request">İşlenecek GetUserListQuery komutu (bu sorgu için genellikle boş bir komut nesnesi).</param>
        /// <param name="cancellationToken">İptal token'ı.</param>
        /// <returns>Kullanıcıların detaylarını içeren KullaniciDto listesi.</returns>
        public async Task<List<KullaniciDto>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            var (kullanicilar, totalCount) = await _kullaniciRepository.GetPagedAsync(
                request.PageNumber,
                request.PageSize,
                request.Email,
                request.Rol,
                cancellationToken);

            // Kullanıcı listesini KullaniciDto listesine dönüştür
            return _mapper.Map<List<KullaniciDto>>(kullanicilar);
        }
    }
}