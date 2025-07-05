using System;

namespace DonationTrackingSystem.Domain.ValueObjects
{
    /// <summary>
    /// Para miktarları için değer nesnesi
    /// </summary>
    public class Money : IEquatable<Money>
    {
        /// <summary>
        /// Para miktarını tutar.
        /// </summary>
        public decimal Amount { get; private set; }

        /// <summary>
        /// Para birimini (örn: TRY, USD) tutar.
        /// </summary>
        public string Currency { get; private set; }

        /// <summary>
        /// Money nesnesi oluşturur ve geçerli olup olmadığını kontrol eder.
        /// </summary>
        /// <param name="amount">Para miktarı</param>
        /// <param name="currency">Para birimi (varsayılan TRY)</param>
        /// <exception cref="ArgumentException">Negatif miktar veya geçersiz para birimi girilirse fırlatılır.</exception>
        public Money(decimal amount, string currency = "TRY")
        {
            // Negatif miktar kontrolü
            if (amount < 0)
                throw new ArgumentException("Para miktarı negatif olamaz.");

            // Para birimi boş olamaz kontrolü
            if (string.IsNullOrWhiteSpace(currency))
                throw new ArgumentException("Para birimi boş olamaz.");

            Amount = amount;
            // Para birimi büyük harfe çevrilerek atanır.
            Currency = currency.ToUpper();
        }

        /// <summary>
        /// İki Money nesnesini toplar. Para birimleri aynı olmalıdır.
        /// </summary>
        /// <param name="left">Birinci Money</param>
        /// <param name="right">İkinci Money</param>
        /// <returns>Toplam Money</returns>
        /// <exception cref="InvalidOperationException">Para birimleri farklıysa fırlatılır.</exception>
        public static Money operator +(Money left, Money right)
        {
            if (left.Currency != right.Currency)
                throw new InvalidOperationException("Farklı para birimleri toplanamaz.");

            return new Money(left.Amount + right.Amount, left.Currency);
        }

        /// <summary>
        /// İki Money nesnesini çıkarır. Para birimleri aynı olmalıdır ve sonuç negatif olamaz.
        /// </summary>
        /// <param name="left">Birinci Money</param>
        /// <param name="right">İkinci Money</param>
        /// <returns>Çıkarma sonucu Money</returns>
        /// <exception cref="InvalidOperationException">Para birimleri farklıysa veya sonuç negatifse fırlatılır.</exception>
        public static Money operator -(Money left, Money right)
        {
            if (left.Currency != right.Currency)
                throw new InvalidOperationException("Farklı para birimleri çıkarılamaz.");

            var result = left.Amount - right.Amount;
            if (result < 0)
                throw new InvalidOperationException("Sonuç negatif olamaz.");

            return new Money(result, left.Currency);
        }

        /// <summary>
        /// İki Money nesnesinin miktar ve para birimi açısından eşitliğini kontrol eder.
        /// Null referanslar için güvenli karşılaştırma yapar.
        /// </summary>
        public static bool operator ==(Money left, Money right)
        {
            if (ReferenceEquals(left, right))
                return true;
            if (left is null || right is null)
                return false;
            return left.Amount == right.Amount && left.Currency == right.Currency;
        }

        /// <summary>
        /// İki Money nesnesinin eşit olmadığını kontrol eder.
        /// </summary>
        public static bool operator !=(Money left, Money right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Money nesnesinin eşitliğini kontrol eder.
        /// </summary>
        /// <param name="obj">Karşılaştırılacak nesne</param>
        /// <returns>Eşitse true, değilse false</returns>
        public override bool Equals(object? obj)
        {
            return Equals(obj as Money);
        }

        /// <summary>
        /// IEquatable<Money> implementasyonu: Money nesnesinin eşitliğini kontrol eder.
        /// </summary>
        public bool Equals(Money? other)
        {
            if (other is null)
                return false;
            return Amount == other.Amount && Currency == other.Currency;
        }

        /// <summary>
        /// Money nesnesinin hash kodunu döndürür.
        /// </summary>
        /// <returns>Hash kodu</returns>
        public override int GetHashCode() => HashCode.Combine(Amount, Currency);

        /// <summary>
        /// Money nesnesinin string temsili.
        /// </summary>
        /// <returns>Örn: 100,00 ₺ TRY</returns>
        public override string ToString() => $"{Amount:C} {Currency}";
    }
}