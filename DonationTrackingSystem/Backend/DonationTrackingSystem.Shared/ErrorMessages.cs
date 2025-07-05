namespace DonationTrackingSystem.Shared
{
    /// <summary>
    /// Servis ve domain katmanında kullanılacak standart hata mesajları sabitleri.
    /// Tüm hata mesajlarını merkezi olarak yönetmek için kullanılır.
    /// </summary>
    public static class ErrorMessages
    {
        // Bağış işlemleri
        public const string BagisNegatifTutar = "ERR_BAGIS_NEGATIF_TUTAR: Bağış tutarı negatif olamaz.";
        public const string BagisciYok = "ERR_BAGISCI_YOK: Bağışçı bulunamadı.";
        public const string BagisTuruGecersiz = "ERR_BAGIS_TURU_GECERSIZ: Geçersiz bağış türü.";
        public const string BagisTarihiGecersiz = "ERR_BAGIS_TARIHI_GECERSIZ: Geçersiz bağış tarihi.";
        public const string BagisLimitAsildi = "ERR_BAGIS_LIMIT_ASILDI: Bağışçı limiti aşıldı.";
        public const string BagisDovizKabulEdilmez = "ERR_BAGIS_DOVIZ: Sadece TRY cinsinden bağış kabul edilmektedir.";
        // Kullanıcı işlemleri
        public const string KullaniciYok = "ERR_KULLANICI_YOK: Kullanıcı bulunamadı.";
        public const string KullaniciYetkisiz = "ERR_KULLANICI_YETKISIZ: Kullanıcının bu işlemi yapmaya yetkisi yok.";
        // Muhasebe işlemleri
        public const string MuhasebeKaydiYok = "ERR_MUHASEBE_YOK: Muhasebe kaydı bulunamadı.";
        public const string MuhasebeKaydiOlusturulamadi = "ERR_MUHASEBE_OLUSTURULAMADI: Muhasebe kaydı oluşturulamadı.";
        // Genel
        public const string GenelBilinmeyenHata = "ERR_GENEL_BILINMEYEN: Bilinmeyen bir hata oluştu.";
    }
}
