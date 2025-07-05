using MediatR;
using DonationTrackingSystem.Application.DTOs;
using DonationTrackingSystem.Application.Interfaces;
using DonationTrackingSystem.Application.Features.Bagis.Commands;
using DonationTrackingSystem.Application.Features.Bagis.Queries;
using AutoMapper;

namespace DonationTrackingSystem.Application.Services
{
    /// <summary>
    /// Bağış işlemleri için uygulama servisinin implementasyonudur.
    /// MediatR komut ve sorgularını kullanarak iş mantığını yürütür.
    /// </summary>
    public class BagisService : IBagisService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        /// <summary>
        /// BagisService sınıfının yapıcı metodu.
        /// </summary>
        /// <param name="mediator">MediatR arayüzü.</param>
        /// <param name="mapper">AutoMapper arayüzü.</param>
        public BagisService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Yeni bir bağış oluşturur.
        /// </summary>
        /// <param name="bagisDto">Oluşturulacak bağışın DTO'su.</param>
        /// <returns>Oluşturulan bağışın ID'si.</returns>
        public async Task<Guid> CreateBagisAsync(BagisDto bagisDto)
        {
            var command = _mapper.Map<CreateBagisCommand>(bagisDto);
            return await _mediator.Send(command);
        }

        /// <summary>
        /// Tüm bağışların listesini getirir.
        /// </summary>
        /// <returns>Bağış DTO'larından oluşan liste.</returns>
        public async Task<List<BagisDto>> GetBagisListAsync()
        {
            var query = new GetBagisListQuery();
            return await _mediator.Send(query);
        }

        /// <summary>
        /// Belirli bir bağışın detaylarını getirir.
        /// </summary>
        /// <param name="id">Bağış ID'si.</param>
        /// <returns>Bağış DTO'su veya null.</returns>
        public async Task<BagisDto?> GetBagisByIdAsync(Guid id)
        {
            var query = new GetBagisDetailQuery { Id = id };
            return await _mediator.Send(query);
        }

        /// <summary>
        /// Belirli bir bağışı günceller.
        /// </summary>
        /// <param name="id">Güncellenecek bağışın ID'si.</param>
        /// <param name="bagisDto">Güncel bağış bilgileri.</param>
        /// <returns>Güncelleme işleminin başarılı olup olmadığını belirten Task.</returns>
        public async Task<bool> UpdateBagisAsync(Guid id, BagisDto bagisDto)
        {
            var command = _mapper.Map<UpdateBagisCommand>(bagisDto);
            command.Id = id; // ID'yi komuta ata
            return await _mediator.Send(command);
        }

        /// <summary>
        /// Belirli bir bağışı siler.
        /// </summary>
        /// <param name="id">Silinecek bağışın ID'si.</param>
        /// <returns>Silme işleminin başarılı olup olmadığını belirten Task.</returns>
        public async Task<bool> DeleteBagisAsync(Guid id)
        {
            var command = new DeleteBagisCommand { Id = id };
            return await _mediator.Send(command);
        }
    }
} 