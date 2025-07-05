using System;
using DonationTrackingSystem.Shared;

namespace DonationTrackingSystem.Domain.Exceptions
{
    /// <summary>
    /// Domain katmanı için temel exception sınıfı.
    /// Tüm domain tabanlı özel hatalar bu sınıftan türetilir.
    /// </summary>
    public class DomainException : Exception
    {
        /// <summary>
        /// Sadece hata mesajı ile exception oluşturur.
        /// </summary>
        /// <param name="message">Hata mesajı</param>
        public DomainException(string message) : base(message)
        {
        }

        /// <summary>
        /// Hata mesajı ve iç hata (inner exception) ile exception oluşturur.
        /// </summary>
        /// <param name="message">Hata mesajı</param>
        /// <param name="innerException">İç hata nesnesi</param>
        public DomainException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    /// <summary>
    /// Bağış işlemleri için özel exception.
    /// Bağışa özgü iş kurallarında hata oluştuğunda fırlatılır.
    /// </summary>
    public class BagisException : DomainException
    {
        /// <summary>
        /// Bağış işlemiyle ilgili hata mesajı ile exception oluşturur.
        /// </summary>
        /// <param name="message">Hata mesajı</param>
        public BagisException(string message) : base(message) { }
        /// <summary>
        /// ErrorMessages sabiti ile exception oluşturur.
        /// </summary>
        public static BagisException NegatifTutar() => new(ErrorMessages.BagisNegatifTutar);
        public static BagisException BagisciYok() => new(ErrorMessages.BagisciYok);
        public static BagisException TuruGecersiz() => new(ErrorMessages.BagisTuruGecersiz);
        public static BagisException TarihiGecersiz() => new(ErrorMessages.BagisTarihiGecersiz);
        public static BagisException LimitAsildi() => new(ErrorMessages.BagisLimitAsildi);
        public static BagisException DovizKabulEdilmez() => new(ErrorMessages.BagisDovizKabulEdilmez);
    }

    /// <summary>
    /// Kullanıcı işlemleri için özel exception.
    /// Kullanıcıya özgü iş kurallarında hata oluştuğunda fırlatılır.
    /// </summary>
    public class KullaniciException : DomainException
    {
        /// <summary>
        /// Kullanıcı işlemiyle ilgili hata mesajı ile exception oluşturur.
        /// </summary>
        /// <param name="message">Hata mesajı</param>
        public KullaniciException(string message) : base(message) { }
        public static KullaniciException KullaniciYok() => new(ErrorMessages.KullaniciYok);
        public static KullaniciException Yetkisiz() => new(ErrorMessages.KullaniciYetkisiz);
    }

    /// <summary>
    /// Muhasebe işlemleri için özel exception.
    /// Muhasebe ile ilgili iş kurallarında hata oluştuğunda fırlatılır.
    /// </summary>
    public class MuhasebeException : DomainException
    {
        /// <summary>
        /// Muhasebe işlemiyle ilgili hata mesajı ile exception oluşturur.
        /// </summary>
        /// <param name="message">Hata mesajı</param>
        public MuhasebeException(string message) : base(message) { }
        public static MuhasebeException KaydiYok() => new(ErrorMessages.MuhasebeKaydiYok);
        public static MuhasebeException Olusturulamadi() => new(ErrorMessages.MuhasebeKaydiOlusturulamadi);
    }
}