using MediatR;
using AutoMapper;
using DonationTrackingSystem.Domain.Repositories;
using DonationTrackingSystem.Domain.Exceptions;
using System.Threading;
using System.Threading.Tasks;
using DonationTrackingSystem.Application.Features.Kullanici.Queries;
using DonationTrackingSystem.Application.DTOs;

namespace DonationTrackingSystem.Application.Features.Kullanici.Handlers
{
    /// <summary>
    /// Kullanıcı detaylarını getirme sorgusunu işleyen MediatR işleyici sınıfı.
    /// Bu sınıf, 'GetUserDetailQuery' komutunu alır ve ilgili kullanıcının detaylarını 'KullaniciDto' olarak döndürür.
    /// CQRS (Command Query Responsibility Segregation) tasarım deseninin 'Query' (Sorgu) tarafını oluşturur.
    /// </summary>
    public class GetUserDetailQueryHandler : IRequestHandler<GetUserDetailQuery, KullaniciDto>
    {
        private readonly IKullaniciRepository _kullaniciRepository; // Kullanıcı verilerine erişim için repository
        private readonly IMapper _mapper; // Nesne dönüşümleri için AutoMapper

        /// <summary>
        /// GetUserDetailQueryHandler sınıfının yapıcı metodu.
        /// Gerekli bağımlılıkları (IKullaniciRepository ve IMapper) enjekte eder.
        /// </summary>
        /// <param name="kullaniciRepository">Kullanıcı veritabanı işlemleri için repository arayüzü.</param>
        /// <param name="mapper">Nesne dönüşümleri için AutoMapper arayüzü.</param>
        public GetUserDetailQueryHandler(IKullaniciRepository kullaniciRepository, IMapper mapper)
        {
            _kullaniciRepository = kullaniciRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// GetUserDetailQuery komutunu işler ve kullanıcının detaylarını döndürür.
        /// </summary>
        /// <param name="request">İşlenecek GetUserDetailQuery komutu.</param>
        /// <param name="cancellationToken">İptal token'ı.</param>
        /// <returns>Kullanıcının detaylarını içeren KullaniciDto nesnesi.</returns>
        /// <exception cref="DomainException">Belirtilen ID'ye sahip kullanıcı bulunamazsa fırlatılır.</exception>
        public async Task<KullaniciDto> Handle(GetUserDetailQuery request, CancellationToken cancellationToken)
        {
            // Kullanıcıyı ID'ye göre veritabanından getir
            var kullanici = await _kullaniciRepository.GetByIdAsync(request.KullaniciId);

            // Kullanıcı bulunamazsa DomainException fırlat
            if (kullanici == null)
            {
                throw new DomainException($"Belirtilen ID'ye sahip kullanıcı bulunamadı: {request.KullaniciId}");
            }

            // Kullanıcı nesnesini KullaniciDto'ya dönüştür
            return _mapper.Map<KullaniciDto>(kullanici);
        }
    }
} 