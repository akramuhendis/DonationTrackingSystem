using System;
using System.Text.RegularExpressions;

namespace DonationTrackingSystem.Domain.ValueObjects
{
    /// <summary>
    /// E-posta adresi için değer nesnesi
    /// </summary>
    public class Email
    {
        /// <summary>
        /// E-posta adresinin gerçek değerini tutar.
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// Email nesnesi oluşturur ve geçerli olup olmadığını kontrol eder.
        /// </summary>
        /// <param name="value">E-posta adresi</param>
        /// <exception cref="ArgumentException">E-posta adresi boş veya geçersizse fırlatılır.</exception>
        public Email(string value)
        {
            // E-posta adresi boş veya sadece boşluklardan oluşuyorsa hata fırlatılır.
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("E-posta adresi boş olamaz.");

            // E-posta adresi geçerli formata sahip değilse hata fırlatılır.
            if (!IsValidEmail(value))
                throw new ArgumentException("Geçersiz e-posta formatı.");

            // E-posta adresi küçük harfe çevrilerek atanır.
            Value = value.ToLowerInvariant();
        }

        /// <summary>
        /// E-posta adresinin geçerli olup olmadığını kontrol eden yardımcı metot.
        /// </summary>
        /// <param name="email">Kontrol edilecek e-posta adresi</param>
        /// <returns>Geçerliyse true, değilse false</returns>
         private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // RFC 5322 standardına uygun e-posta regex paterni
                string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                return Regex.IsMatch(email, pattern);
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        /// <summary>
        /// Email nesnesini string'e otomatik olarak dönüştürür.
        /// </summary>
        /// <param name="email">Email nesnesi</param>
        public static implicit operator string(Email email) => email.Value;


        /// <summary>
        /// String değeri Email nesnesine açıkça dönüştürür.
        /// </summary>
        /// <param name="value">E-posta adresi</param>
        public static explicit operator Email(string value) => new Email(value);

        /// <summary>
        /// Email nesnesinin eşitliğini kontrol eder.
        /// </summary>
        /// <param name="obj">Karşılaştırılacak nesne</param>
        /// <returns>Eşitse true, değilse false</returns>
        public override bool Equals(object? obj)
        {
            if (obj is Email other)
                return Value == other.Value;
            return false;
        }

        /// <summary>
        /// Email nesnesinin hash kodunu döndürür.
        /// </summary>
        /// <returns>Hash kodu</returns>
        public override int GetHashCode() => Value.GetHashCode();

        /// <summary>
        /// Email nesnesinin string temsili.
        /// </summary>
        /// <returns>E-posta adresi</returns>
        public override string ToString() => Value;
    }
}