using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DonationTrackingSystem.Application.Exceptions
{
    /// <summary>
    /// Uygulama katmanında doğrulama hatalarını temsil eden özel bir istisna sınıfıdır.
    /// FluentValidation kütüphanesi tarafından üretilen doğrulama hatalarını toplamak ve yapılandırılmış bir şekilde sunmak için kullanılır.
    /// Bu istisna, genellikle MediatR pipeline'ındaki <see cref="ValidationBehavior{TRequest, TResponse}"/> tarafından fırlatılır.
    /// </summary>
    public class ValidationException : Exception
    {
        /// <summary>
        /// Doğrulama hatalarını, özellik adına göre gruplandırılmış bir sözlük olarak tutar.
        /// Anahtar (string): Hatanın ilişkili olduğu özelliğin adı (örneğin, "Email", "Password").
        /// Değer (string[]): İlgili özellik için oluşan hata mesajlarının bir dizisi.
        /// </summary>
        public IDictionary<string, string[]> Errors { get; }

        /// <summary>
        /// <see cref="ValidationException"/> sınıfının parametresiz yapıcı metodudur.
        /// Varsayılan bir hata mesajı ile istisnayı başlatır ve boş bir hata sözlüğü oluşturur.
        /// </summary>
        public ValidationException() : base("Bir veya daha fazla doğrulama hatası oluştu.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        /// <summary>
        /// <see cref="ValidationException"/> sınıfının doğrulama hataları koleksiyonu alan yapıcı metodudur.
        /// FluentValidation tarafından üretilen <see cref="ValidationFailure"/> nesnelerini alır
        /// ve bunları özellik adına göre gruplandırarak <see cref="Errors"/> sözlüğüne dönüştürür.
        /// </summary>
        /// <param name="failures">FluentValidation tarafından döndürülen doğrulama hatalarının bir koleksiyonu.</param>
        public ValidationException(IEnumerable<ValidationFailure> failures) : this() // Parametresiz yapıcıyı çağırır.
        {
            // Hataları özellik adına göre gruplandırır.
            // Örneğin, "Email" özelliği için birden fazla hata mesajı olabilir.
            var failureGroups = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage);

            // Her bir hata grubunu (özellik ve o özelliğe ait hata mesajları) döngüye alır.
            foreach (var failureGroup in failureGroups)
            {
                var propertyName = failureGroup.Key; // Özellik adı (örneğin, "Email")
                var propertyFailures = failureGroup.ToArray(); // O özelliğe ait tüm hata mesajları dizisi

                // Hata sözlüğüne özelliği ve hata mesajlarını ekler.
                Errors.Add(propertyName, propertyFailures);
            }
        }
    }
}
