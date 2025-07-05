using MediatR;
using System.Collections.Generic;
using DonationTrackingSystem.Application.DTOs;

namespace DonationTrackingSystem.Application.Features.Bagis.Queries
{
    /// <summary>
    /// Bağışların listesini sayfalanmış olarak almak için kullanılan sorguyu temsil eder.
    /// Bu sınıf, CQRS deseni içinde MediatR tarafından yönetilir.
    /// Bu sorgu gönderildiğinde, GetBagisListQueryHandler tarafından işlenir.
    /// </summary>
    public class GetBagisListQuery : IRequest<List<BagisDto>>
    {
        /// <summary>
        /// Getirilecek sayfa numarası. Varsayılan değer 1'dir.
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// Sayfa başına düşecek kayıt sayısı. Varsayılan değer 10'dur.
        /// </summary>
        public int PageSize { get; set; } = 10;
    }
} 