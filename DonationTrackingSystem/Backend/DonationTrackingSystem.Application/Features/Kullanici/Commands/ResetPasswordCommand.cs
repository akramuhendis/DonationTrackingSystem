using MediatR;

namespace DonationTrackingSystem.Application.Features.Kullanici.Commands
{
    /// <summary>
    /// Kullanıcı şifresini sıfırlama işlemini temsil eden MediatR komut sınıfı.
    /// Bu komut, CQRS (Command Query Responsibility Segregation) tasarım deseninin 'Command' (Komut) tarafını oluşturur.
    /// Bir kullanıcının unuttuğu şifresini, genellikle e-posta ile gönderilen bir sıfırlama token'ı kullanarak yeni bir şifre ile değiştirmesini sağlar.
    /// Bu komut, ilgili 'ResetPasswordCommandHandler' tarafından işlenerek sıfırlama token'ının doğrulanması,
    /// yeni şifrenin hash'lenmesi ve kullanıcının veritabanındaki şifresinin güncellenmesi gibi süreçleri tetikler.
    /// Başarılı bir şifre sıfırlama işleminin ardından, istemciye geri döndürülecek bir onay mesajı üretilmesini sağlar.
    /// </summary>
    public class ResetPasswordCommand : IRequest<bool> // IRequest<string> arayüzü, bu komutun işlendikten sonra bir 'string' (örneğin, başarı mesajı) tipinde bir sonuç döndüreceğini belirtir.
    {
        /// <summary>
        /// Şifresi sıfırlanacak kullanıcının e-posta adresi.
        /// Bu alan, kullanıcının kimliğini doğrulamak ve sıfırlama token'ını eşleştirmek için kullanılır.
        /// 'required' anahtar kelimesi, bu özelliğin 'ResetPasswordCommand' nesnesi oluşturulurken mutlaka bir değer alması gerektiğini belirtir.
        /// </summary>
        public required string Email { get; set; }

        /// <summary>
        /// E-posta ile kullanıcıya gönderilen benzersiz şifre sıfırlama token'ı.
        /// Bu token, şifre sıfırlama isteğinin geçerliliğini ve yetkisini doğrulamak için kullanılır.
        /// 'required' anahtar kelimesi, bu özelliğin 'ResetPasswordCommand' nesnesi oluşturulurken mutlaka bir değer alması gerektiğini belirtir.
        /// </summary>
        public required string ResetToken { get; set; }

        /// <summary>
        /// Kullanıcının belirleyeceği yeni şifre.
        /// Bu şifre, güvenlik nedeniyle hiçbir zaman düz metin olarak saklanmamalıdır; veritabanına kaydedilmeden önce mutlaka hash'lenmelidir.
        /// 'required' anahtar kelimesi, bu özelliğin 'ResetPasswordCommand' nesnesi oluşturulurken mutlaka bir değer alması gerektiğini belirtir.
        /// Şifre karmaşıklığı ve uzunluğu gibi güvenlik politikalarına uygunluğu doğrulanmalıdır.
        /// </summary>
        public required string NewPassword { get; set; }

        /// <summary>
        /// Yeni şifrenin tekrarı.
        /// Kullanıcının yeni şifreyi doğru girdiğinden emin olmak için kullanılır.
        /// Bu alanın 'NewPassword' alanı ile aynı olması beklenir.
        /// 'required' anahtar kelimesi, bu özelliğin 'ResetPasswordCommand' nesnesi oluşturulurken mutlaka bir değer alması gerektiğini belirtir.
        /// </summary>
        public required string ConfirmNewPassword { get; set; }
    }
}