using DonationTrackingSystem.Domain.Exceptions;
using MediatR;
using DonationTrackingSystem.Domain.Repositories;
using DonationTrackingSystem.Application.Features.Bagis.Commands;

namespace DonationTrackingSystem.Application.Features.Bagis.Handlers
{
    // DeleteBagisCommandHandler, belirli bir bağışı silmek için MediatR komutunu işleyen handler'dır.
    public class DeleteBagisCommandHandler : IRequestHandler<DeleteBagisCommand, bool>
    {
        // Bağış repository'si (veritabanı işlemleri için)
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBagisCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Handle metodu, DeleteBagisCommand komutunu işler ve silme işleminin başarılı olup olmadığını bool olarak döndürür
        public async Task<bool> Handle(DeleteBagisCommand request, CancellationToken cancellationToken)
        {
            // Silinecek bağış veritabanında Id ile aranır
            var bagis = await _unitOfWork.BagisRepository.GetByIdAsync(request.Id);
            if (bagis == null)
            {
                throw new DomainException("Bağış bulunamadı.");
            }
            await _unitOfWork.BagisRepository.DeleteAsync(bagis.Id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}