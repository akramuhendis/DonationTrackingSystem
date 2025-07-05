using System;
using System.Threading;
using System.Threading.Tasks;
using DonationTrackingSystem.Domain.Entities;

namespace DonationTrackingSystem.Domain.Repositories
{
    /// <summary>
    /// Unit of Work pattern için arayüz.
    /// Tüm repository'leri ve transaction yönetimini tek bir noktada toplar.
    /// Böylece işlemler bir bütün olarak yönetilebilir ve atomik olarak kaydedilebilir.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Bağış işlemlerine özel repository.
        /// Bağış ekleme, güncelleme, silme ve raporlama işlemleri için kullanılır.
        /// </summary>
        IBagisRepository BagisRepository { get; }

        /// <summary>
        /// Kullanıcı işlemlerine özel repository.
        /// Sistemdeki kullanıcıların yönetimi için kullanılır.
        /// </summary>
        IKullaniciRepository KullaniciRepository { get; }

        /// <summary>
        /// Bağışçı işlemlerine özel repository.
        /// Bağışçıların eklenmesi, güncellenmesi ve sorgulanması için kullanılır.
        /// </summary>
        IBagisciRepository BagisciRepository { get;} 

        /// <summary>
        /// Muhasebe fiş işlemlerine özel repository.
        /// Muhasebe kayıtlarının yönetimi için kullanılır.
        /// </summary>
        IMuhasebeFisRepository MuhasebeFisRepository { get; }

        /// <summary>
        /// Hesap planı işlemlerine özel repository.
        /// Muhasebe hesap ağacının yönetimi için kullanılır.
        /// </summary>
        IHesapPilaniRepository HesapPilaniRepository { get; }

        /// <summary>
        /// Kasa ve banka işlemlerine özel repository.
        /// Kasa ve banka hesaplarının yönetimi için kullanılır.
        /// </summary>
        IKasaBankaRepository KasaBankaRepository { get; }

        /// <summary>
        /// Yapılan tüm değişiklikleri veritabanına kaydeder.
        /// Transaction yönetimiyle birlikte kullanılırsa, işlemler atomik olarak kaydedilir.
        /// </summary>
        /// <param name="cancellationToken">İptal token'ı (uzun süren işlemler için)</param>
        /// <returns>Kaydedilen kayıt sayısı</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Transaction başlatır. Birden fazla işlemin tek transaction içinde yapılmasını sağlar.
        /// </summary>
        /// <param name="cancellationToken">İptal token'ı</param>
        Task BeginTransactionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Transaction'ı commit eder. Tüm işlemler başarılıysa kalıcı olarak kaydedilir.
        /// </summary>
        /// <param name="cancellationToken">İptal token'ı</param>
        Task CommitTransactionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Transaction'ı rollback eder. Hata oluşursa yapılan işlemler geri alınır.
        /// </summary>
        /// <param name="cancellationToken">İptal token'ı</param>
        Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
    }
}