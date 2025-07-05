using DonationTrackingSystem.Domain.Exceptions;
using MediatR;
using DonationTrackingSystem.Domain.Repositories;
using DonationTrackingSystem.Application.Features.Kullanici.Commands;

namespace DonationTrackingSystem.Application.Features.Kullanici.Handlers
{
    /// <summary>
    /// Kullanıcı silme komutunu (DeleteUserCommand) işleyen MediatR işleyicisidir.
    /// </summary>
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var kullanici = await _unitOfWork.KullaniciRepository.GetByIdAsync(request.Id);
            if (kullanici == null)
            {
                throw new DomainException("Kullanıcı bulunamadı.");
            }

            await _unitOfWork.KullaniciRepository.DeleteAsync(kullanici.Id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
