using MediatR;
using DonationTrackingSystem.Application.DTOs;
using System;

namespace DonationTrackingSystem.Application.Features.Bagis.Queries
{
    // Belirli bir bağışın detayını getirmek için kullanılan query
    public class GetBagisDetailQuery : IRequest<BagisDto?>
    {
        // Detayı istenen bağışın benzersiz kimliği
        public Guid Id { get; set; }
    }
}
