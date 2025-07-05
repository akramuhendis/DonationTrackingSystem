using AutoMapper;
using MediatR;
using DonationTrackingSystem.Domain.Entities;
using DonationTrackingSystem.Domain.Repositories;
using DonationTrackingSystem.Application.Features.Bagis.Commands;
using DonationTrackingSystem.Domain.Exceptions;

namespace DonationTrackingSystem.Application.Features.Bagis.Handlers
{
    // CreateBagisCommandHandler, yeni bir bağış kaydı oluşturmak için MediatR komutunu işleyen handler'dır.
    // Bu handler, CQRS deseninde komutun işlenmesini ve veritabanına kaydedilmesini sağlar.
    public class CreateBagisCommandHandler : IRequestHandler<CreateBagisCommand, Guid>
    {
        // Bağış repository'si, bağış verilerinin veritabanı işlemlerini gerçekleştirmek için kullanılır.
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateBagisCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // Handle metodu, CreateBagisCommand komutunu işler ve yeni oluşturulan bağışın Guid Id'sini döndürür.
        // Bu metod, MediatR tarafından otomatik olarak çağrılır.
        public async Task<Guid> Handle(CreateBagisCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Komuttan Bagis entity'sine dönüşüm yapılır.
                // Tam namespace ile kullanılarak olası isim çakışmaları önlenir.
                var bagis = _mapper.Map<DonationTrackingSystem.Domain.Entities.Bagis>(request);
                await _unitOfWork.BagisRepository.AddAsync(bagis);
                await _unitOfWork.SaveChangesAsync();
                return bagis.Id;
            }
            catch (Exception ex)
            {
                // Hata loglama mekanizması burada kullanılabilir.
                // Örneğin: _logger.LogError(ex, "Bağış oluşturulurken bir hata oluştu.");
                throw new DomainException("Bağış oluşturulurken bir hata oluştu: " + ex.Message, ex);
            }
        }
    }
}