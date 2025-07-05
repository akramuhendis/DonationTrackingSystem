using MediatR; // MediatR kütüphanesi, CQRS (Command Query Responsibility Segregation) desenini uygulamak için kullanılır. Komutların ve sorguların işlenmesini sağlar.
using AutoMapper; // AutoMapper kütüphanesi, nesneler arasında otomatik eşleme (mapping) yapmak için kullanılır. DTO'lar ve varlıklar arasındaki dönüşümleri kolaylaştırır.
using DonationTrackingSystem.Domain.Entities; // Domain katmanındaki varlık (entity) tanımlarına erişim sağlar. Özellikle Kullanici varlığı burada kullanılır.
using DonationTrackingSystem.Domain.Repositories; // Domain katmanındaki repository arayüzlerine erişim sağlar. Veritabanı işlemleri için IKullaniciRepository kullanılır.
using DonationTrackingSystem.Application.Features.Kullanici.Commands; // Kullanıcı ile ilgili komut tanımlarına erişim sağlar. RegisterUserCommand burada işlenir.
using DonationTrackingSystem.Domain.Exceptions;
using DonationTrackingSystem.Domain.Services;

namespace DonationTrackingSystem.Application.Features.Kullanici.Handlers
{
    /// <summary>
    /// Yeni bir kullanıcı kaydı komutunu (RegisterUserCommand) işleyen MediatR işleyicisidir.
    /// IRequestHandler arayüzünü uygulayarak, belirli bir komut (RegisterUserCommand) için belirli bir yanıt (Guid) döndürme yeteneği sağlar.
    /// Bu sınıf, gelen kullanıcı kayıt isteğini alır, iş mantığını uygular (DTO'dan varlığa dönüştürme, veritabanına kaydetme) ve işlemin sonucunu döndürür.
    /// </summary>
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Guid>
    {
        // IKullaniciRepository arayüzü, kullanıcı veritabanı işlemlerini soyutlar.
        // Dependency Injection (Bağımlılık Enjeksiyonu) prensibiyle dışarıdan sağlanır.
        private readonly IKullaniciRepository _kullaniciRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// RegisterUserCommandHandler sınıfının yapıcı metodudur.
        /// Gerekli bağımlılıkları (IKullaniciRepository, IMapper, IPasswordHasher) enjekte eder.
        /// Bu sayede sınıf, veritabanı işlemleri ve nesne eşleme yeteneklerine sahip olur.
        /// </summary>
        /// <param name="kullaniciRepository">Kullanıcı veritabanı işlemleri için repository arayüzü.</param>
        /// <param name="mapper">Nesne eşleme işlemleri için AutoMapper arayüzü.</param>
        /// <param name="passwordHasher">Şifre hashleme işlemleri için servis arayüzü.</param>
        public RegisterUserCommandHandler(IKullaniciRepository kullaniciRepository, IMapper mapper, IPasswordHasher passwordHasher, IUnitOfWork unitOfWork)
        {
            _kullaniciRepository = kullaniciRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// RegisterUserCommand komutunu asenkron olarak işleyen metodudur.
        /// MediatR kütüphanesi tarafından çağrılır ve komutun ana iş mantığını içerir.
        /// </summary>
        /// <param name="request">İşlenecek olan RegisterUserCommand nesnesi. Yeni kullanıcı kayıt bilgilerini içerir.</param>
        /// <param name="cancellationToken">Asenkron işlemlerde iptal sinyali için kullanılır.</param>
        /// <returns>Kayıt edilen kullanıcının benzersiz kimlik numarası (Guid) döner.</returns>
        /// <exception cref="DomainException">E-posta zaten kayıtlıysa fırlatılır.</exception>
        public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            // E-posta benzersizliğini kontrol et
            var existingUser = await _kullaniciRepository.GetByEmailAsync(request.UserRegister.Email);
            if (existingUser != null)
            {
                throw new DomainException("Bu e-posta adresi zaten kayıtlı.");
            }

            // AutoMapper kullanarak RegisterUserCommand nesnesini Kullanici domain varlığına eşler.
            var kullanici = _mapper.Map<DonationTrackingSystem.Domain.Entities.Kullanici>(request.UserRegister);

            // Şifreyi hashle
            kullanici.SifreHash = _passwordHasher.HashPassword(request.UserRegister.Sifre);

            // Güvenlik nedeniyle, kayıt sırasında doğrudan rol ataması yerine varsayılan bir rol atanması önerilir.
            // Eğer UserRegisterDto'da bir rol belirtilmişse ve bu işlem yetkili bir kullanıcı tarafından yapılıyorsa
            // o rol atanabilir. Aksi takdirde varsayılan rol atanır.
            kullanici.Rol = DonationTrackingSystem.Domain.Enums.Rol.Gonullu; // Varsayılan rol ataması
            // TODO: Eğer admin tarafından kayıt yapılıyorsa ve farklı bir rol atanmak isteniyorsa, burada yetkilendirme kontrolü yapılmalı.

            // Eşlenen Kullanici varlığını asenkron olarak veritabanına ekler.
            await _kullaniciRepository.AddAsync(kullanici);
            await _unitOfWork.SaveChangesAsync();

            // Başarılı bir kayıt işleminden sonra, yeni oluşturulan kullanıcının benzersiz kimlik numarasını (Id) döndürür.
            return kullanici.Id;
        }
    }
}