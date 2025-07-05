using DonationTrackingSystem.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DonationTrackingSystem.Application.Interfaces
{
    /// <summary>
    /// Bağış işlemleri için uygulama servis arayüzü.
    /// Bu arayüz, bağışlarla ilgili temel CRUD (Create, Read, Update, Delete) operasyonlarını tanımlar.
    /// </summary>
    public interface IBagisService
    {
        /// <summary>
        /// Asenkron olarak yeni bir bağış oluşturur.
        /// </summary>
        /// <param name="bagisDto">Oluşturulacak bağışın veri transfer nesnesi (DTO).</param>
        /// <returns>Oluşturulan bağışın ID'sini içeren bir Task.</returns>
        Task<Guid> CreateBagisAsync(BagisDto bagisDto);

        /// <summary>
        /// Sistemdeki tüm bağışların listesini asenkron olarak getirir.
        /// </summary>
        /// <returns>Bağış DTO'larından oluşan bir listeyi içeren bir Task.</returns>
        Task<List<BagisDto>> GetBagisListAsync();

        /// <summary>
        /// Belirtilen ID'ye sahip bağışı asenkron olarak getirir.
        /// </summary>
        /// <param name="id">Getirilecek bağışın ID'si.</param>
        /// <returns>Bulunan bağışın DTO'sunu veya bulunamazsa null içeren bir Task.</returns>
        Task<BagisDto?> GetBagisByIdAsync(Guid id);

        /// <summary>
        /// Belirtilen ID'ye sahip bağışı asenkron olarak günceller.
        /// </summary>
        /// <param name="id">Güncellenecek bağışın ID'si.</param>
        /// <param name="bagisDto">Güncel bağış bilgilerini içeren DTO.</param>
        /// <returns>Operasyonun tamamlandığını belirten bir Task.</returns>
        Task<bool> UpdateBagisAsync(Guid id, BagisDto bagisDto);

        /// <summary>
        /// Belirtilen ID'ye sahip bağışı asenkron olarak siler.
        /// </summary>
        /// <param name="id">Silinecek bağışın ID'si.</param>
        /// <returns>Operasyonun tamamlandığını belirten bir Task.</returns>
        Task<bool> DeleteBagisAsync(Guid id);
    }
}
