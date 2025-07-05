using AutoMapper;
using MediatR;
using DonationTrackingSystem.Domain.Repositories;
using DonationTrackingSystem.Application.DTOs;
using DonationTrackingSystem.Application.Features.Bagis.Queries;

namespace DonationTrackingSystem.Application.Features.Bagis.Handlers
{
    // GetBagisDetailQueryHandler, belirli bir bağışın detayını getirmek için MediatR sorgusunu işleyen handler'dır.
    // Bu handler, CQRS deseninde sorgunun işlenmesini ve DTO olarak döndürülmesini sağlar.
    public class GetBagisDetailQueryHandler : IRequestHandler<GetBagisDetailQuery, BagisDto?>
    {
        // Bağış repository'si, bağış verilerinin veritabanı işlemlerini gerçekleştirmek için kullanılır.
        private readonly IBagisRepository _bagisRepository;
        // AutoMapper, entity ile DTO arasında dönüşüm yapmak için kullanılır.
        private readonly IMapper _mapper;

        // Constructor, bağımlılıkların (repository ve mapper) dışarıdan alınmasını sağlar (Dependency Injection).
        public GetBagisDetailQueryHandler(IBagisRepository bagisRepository, IMapper mapper)
        {
            _bagisRepository = bagisRepository; // Repository bağımlılığı atanır.
            _mapper = mapper; // Mapper bağımlılığı atanır.
        }

        // Handle metodu, GetBagisDetailQuery sorgusunu işler ve ilgili bağışın DTO'sunu döndürür.
        // Eğer bağış bulunamazsa null döndürülür.
        public async Task<BagisDto?> Handle(GetBagisDetailQuery request, CancellationToken cancellationToken)
        {
            // Bağış entity'si, repository üzerinden Id ile veritabanında aranır.
            var bagis = await _bagisRepository.GetByIdAsync(request.Id);
            // Eğer bağış bulunamazsa null döndürülür (kullanıcıya "bulunamadı" bilgisi verilebilir).
            if (bagis == null) return null;
            // Bağış entity'si, AutoMapper ile DTO'ya dönüştürülerek döndürülür.
            return _mapper.Map<BagisDto>(bagis);
        }
    }
}

