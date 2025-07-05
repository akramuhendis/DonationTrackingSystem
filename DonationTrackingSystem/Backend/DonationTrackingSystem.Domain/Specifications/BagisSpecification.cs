using System;
using System.Linq.Expressions;
using DonationTrackingSystem.Domain.Entities;
using DonationTrackingSystem.Domain.Enums;

namespace DonationTrackingSystem.Domain.Specifications
{
    /// <summary>
    /// Bağış varlıkları için çeşitli filtreleme, sıralama ve dahil etme işlemlerini tanımlayan specification sınıfı.
    /// Repository pattern ile birlikte kullanılarak sorguların merkezi olarak yönetilmesini sağlar.
    /// </summary>
    public class BagisSpecification : ISpecification<Bagis>
    {
        /// <summary>
        /// Bağışlar üzerinde uygulanacak filtre kriterini temsil eder.
        /// Örneğin: x => x.BagisciId == ... && !x.IsDeleted
        /// </summary>
        public Expression<Func<Bagis, bool>> Criteria { get; }

        /// <summary>
        /// Sorguya dahil edilecek ilişkili varlıkların listesini tutar (örn: Bagisci).
        /// Eager loading için kullanılır.
        /// </summary>
        public List<Expression<Func<Bagis, object>>> Includes { get; } = new();

        /// <summary>
        /// Sorgu sonucunun artan şekilde hangi alana göre sıralanacağını belirtir.
        /// Örneğin: x => x.Tutar
        /// </summary>
        public Expression<Func<Bagis, object>>? OrderBy { get; private set; }

        /// <summary>
        /// Sorgu sonucunun azalan şekilde hangi alana göre sıralanacağını belirtir.
        /// Örneğin: x => x.BagisTaihi
        /// </summary>
        public Expression<Func<Bagis, object>>? OrderByDescending { get; private set; }

        /// <summary>
        /// Sorguda kaç kaydın atlanacağını belirtir (sayfalama için).
        /// </summary>
        public int Skip { get; private set; }

        /// <summary>
        /// Sorguda kaç kaydın alınacağını belirtir (sayfalama için).
        /// </summary>
        public int Take { get; private set; }

        /// <summary>
        /// Sayfalama işleminin aktif olup olmadığını belirtir.
        /// </summary>
        public bool IsPagingEnabled { get; private set; }

        /// <summary>
        /// Varsayılan kurucu. Silinmemiş tüm bağışları getirir.
        /// </summary>
        public BagisSpecification()
        {
            // Sadece silinmemiş bağışlar filtrelenir
            Criteria = x => !x.IsDeleted;
        }

        /// <summary>
        /// Belirli bir bağışçıya ait silinmemiş bağışları getirir ve bağışçı bilgisini dahil eder.
        /// </summary>
        /// <param name="bagisciId">Bağışçı Id'si</param>
        public BagisSpecification(Guid bagisciId)
        {
            // Bağışçıya göre filtreleme ve eager loading
            Criteria = x => x.BagisciId == bagisciId && !x.IsDeleted;
            Includes.Add(x => x.Bagisci!);
        }

        /// <summary>
        /// Belirli bir tarih aralığındaki silinmemiş bağışları getirir ve bağış tarihine göre azalan sıralar.
        /// </summary>
        /// <param name="startDate">Başlangıç tarihi</param>
        /// <param name="endDate">Bitiş tarihi</param>
        public BagisSpecification(DateTime startDate, DateTime endDate)
        {
            // Tarih aralığına göre filtreleme ve azalan sıralama
            Criteria = x => x.BagisTarihi >= startDate && x.BagisTarihi <= endDate && !x.IsDeleted;
            OrderByDescending = x => x.BagisTarihi;
        }

        /// <summary>
        /// Belirli bir bağış türüne sahip silinmemiş bağışları getirir ve bağış tarihine göre azalan sıralar.
        /// </summary>
        /// <param name="bagisTuru">Bağış türü</param>
        public BagisSpecification(BagisTuru bagisTuru)
        {
            // Bağış türüne göre filtreleme ve azalan sıralama
            Criteria = x => x.BagisTuru == bagisTuru && !x.IsDeleted;
            OrderByDescending = x => x.BagisTarihi;
        }

        /// <summary>
        /// Belirli bir tutar aralığındaki silinmemiş bağışları getirir ve tutara göre artan sıralar.
        /// </summary>
        /// <param name="minAmount">Minimum tutar</param>
        /// <param name="maxAmount">Maksimum tutar</param>
        public BagisSpecification(decimal minAmount, decimal maxAmount)
        {
            // Tutar aralığına göre filtreleme ve artan sıralama
            Criteria = x => x.Tutar >= minAmount && x.Tutar <= maxAmount && !x.IsDeleted;
            OrderBy = x => x.Tutar;
        }

        /// <summary>
        /// Sorguya ilişkili bir varlık ekler (örn: Bagisci).
        /// Eager loading için kullanılır.
        /// </summary>
        /// <param name="includeExpression">Dahil edilecek varlık</param>
        public void AddInclude(Expression<Func<Bagis, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        /// <summary>
        /// Sorguya sayfalama uygular.
        /// </summary>
        /// <param name="skip">Atlanacak kayıt sayısı</param>
        /// <param name="take">Alınacak kayıt sayısı</param>
        public void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }

        /// <summary>
        /// Sorguya artan sıralama uygular.
        /// </summary>
        /// <param name="orderByExpression">Sıralanacak alan</param>
        public void ApplyOrderBy(Expression<Func<Bagis, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        /// <summary>
        /// Sorguya azalan sıralama uygular.
        /// </summary>
        /// <param name="orderByDescExpression">Sıralanacak alan</param>
        public void ApplyOrderByDescending(Expression<Func<Bagis, object>> orderByDescExpression)
        {
            OrderByDescending = orderByDescExpression;
        }
    }
}