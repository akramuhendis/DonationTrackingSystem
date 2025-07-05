using AutoMapper;
using DonationTrackingSystem.Application.DTOs;
using DonationTrackingSystem.Domain.Entities;
using DonationTrackingSystem.Application.Features.Bagis.Commands;
using DonationTrackingSystem.Application.Features.Kullanici.Commands;

namespace DonationTrackingSystem.Application.Mappings
{
    // ==================================================================================================
    // MappingProfile Sınıfı
    // ==================================================================================================
    // AMAÇ: Bu sınıf, AutoMapper kütüphanesi için merkezi yapılandırma profili olarak görev yapar.
    // AutoMapper, farklı katmanlardaki nesneleri (örneğin, Domain katmanındaki Entity'ler ve 
    // Application katmanındaki DTO'lar) birbirine otomatik olarak dönüştürmemizi sağlar. 
    // Bu, kod tekrarını azaltır ve kodun daha temiz, okunabilir olmasını sağlar.
    // ==================================================================================================
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // ==================================================
            // BAĞIŞ (BAGIS) İLE İLGİLİ DÖNÜŞÜMLER
            // ==================================================

            // ------------------------------------------------------------------------------------------
            // Dönüşüm: Bagis (Entity) <---> BagisDto (Data Transfer Object)
            // ------------------------------------------------------------------------------------------
            // AÇIKLAMA: Bu kural, veritabanı varlığı olan 'Bagis' ile veri taşıma nesnesi olan 'BagisDto' 
            // arasında çift yönlü bir dönüşüm tanımlar. 
            // - 'Bagis' -> 'BagisDto': Veritabanından gelen veriyi API'ye veya sunum katmanına göndermek için.
            // - 'BagisDto' -> 'Bagis': API'den gelen veriyi işleyip veritabanına kaydetmek için.
            // '.ReverseMap()' metodu bu çift yönlü dönüşümü otomatik olarak yapılandırır.
            // ------------------------------------------------------------------------------------------
            CreateMap<Bagis, BagisDto>().ReverseMap();

            // ------------------------------------------------------------------------------------------
            // Dönüşüm: CreateBagisCommand ---> Bagis (Entity)
            // ------------------------------------------------------------------------------------------
            // AÇIKLAMA: Bu kural, "yeni bağış oluşturma" isteğini temsil eden 'CreateBagisCommand' 
            // nesnesini, veritabanına eklenecek olan 'Bagis' varlığına dönüştürür. Bu, CQRS 
            // (Command Query Responsibility Segregation) deseninin Command kısmını uygulamamıza yardımcı olur.
            // Bu dönüşüm tek yönlüdür çünkü bir varlığı komuta dönüştürmek anlamsızdır.
            // ------------------------------------------------------------------------------------------
            CreateMap<CreateBagisCommand, Bagis>();

            CreateMap<UpdateBagisCommand, Bagis>();


            // ==================================================
            // KULLANICI (KULLANICI) İLE İLGİLİ DÖNÜŞÜMLER
            // ==================================================

            // ------------------------------------------------------------------------------------------
            // Dönüşüm: Kullanici (Entity) <---> KullaniciDto (Data Transfer Object)
            // ------------------------------------------------------------------------------------------
            // AÇIKLAMA: Bu kural, 'Kullanici' veritabanı varlığı ile 'KullaniciDto' arasında çift yönlü 
            // bir dönüşüm sağlar. Bu, kullanıcı listeleme, detay görüntüleme, profil güncelleme gibi 
            // temel kullanıcı işlemleri için kritik öneme sahiptir.
            // ÖNEMİ: Bu kural olmadan, servis katmanı veritabanından aldığı 'Kullanici' nesnesini 
            // güvenli bir şekilde dış dünyaya sunamaz (örneğin, şifre hash'i gibi hassas verileri sızdırabilir).
            // DTO'lar, sadece gerekli ve güvenli verilerin taşınmasını sağlar.
            // ------------------------------------------------------------------------------------------
            CreateMap<Kullanici, KullaniciDto>().ReverseMap()
                .ForMember(dest => dest.SifreHash, opt => opt.Ignore());

            CreateMap<UserRegisterDto, Kullanici>();
            CreateMap<UserRegisterDto, RegisterUserCommand>();

            CreateMap<UserUpdateDto, UpdateUserCommand>();
            CreateMap<UpdateUserCommand, Kullanici>();
        }
    }
}
