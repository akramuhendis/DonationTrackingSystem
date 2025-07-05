using FluentValidation;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ValidationException = DonationTrackingSystem.Application.Exceptions.ValidationException;

namespace DonationTrackingSystem.Application.Behaviors
{
    /// <summary>
    /// MediatR pipeline'ına entegre edilmiş bir doğrulama davranışıdır.
    /// Bu davranış, bir MediatR isteği (request) işlenmeden önce, ilgili istekle ilişkili tüm FluentValidation doğrulayıcılarını çalıştırır.
    /// Eğer doğrulama hataları bulunursa, bir <see cref="ValidationException"/> fırlatır ve isteğin işlenmesini durdurur.
    /// Bu sayede, iş mantığına geçmeden önce tüm giriş verilerinin geçerliliği sağlanmış olur.
    /// </summary>
    /// <typeparam name="TRequest">İşlenecek MediatR isteğinin tipi. <see cref="IRequest{TResponse}"/> arayüzünü uygulamalıdır.</typeparam>
    /// <typeparam name="TResponse">MediatR isteğinin döndüreceği yanıtın tipi.</typeparam>
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse> // TRequest'in bir MediatR isteği olduğunu belirtir.
    {
        /// <summary>
        /// Belirli bir <typeparamref name="TRequest"/> tipi için kayıtlı tüm FluentValidation doğrulayıcılarını içerir.
        /// Dependency Injection (DI) konteyneri tarafından otomatik olarak enjekte edilir.
        /// </summary>
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        /// <summary>
        /// <see cref="ValidationBehavior{TRequest, TResponse}"/> sınıfının yapıcı metodudur.
        /// </summary>
        /// <param name="validators">DI konteynerinden enjekte edilen doğrulayıcıların koleksiyonu.</param>
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        /// <summary>
        /// MediatR pipeline'ının ana işleme metodudur.
        /// Bir istek geldiğinde bu metot çağrılır ve isteğin işlenmesinden önce doğrulama mantığını uygular.
        /// </summary>
        /// <param name="request">İşlenecek olan MediatR isteği.</param>
        /// <param name="next">Pipeline'daki bir sonraki işleyiciye veya nihai işleyiciye (handler) delege.</param>
        /// <param name="cancellationToken">Asenkron işlemlerde iptal sinyali için kullanılır.</param>
        /// <returns>İsteğin işlenmesi sonucunda dönen yanıt.</returns>
        /// <exception cref="ValidationException">Eğer doğrulama hataları bulunursa fırlatılır.</exception>
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            // Eğer bu istek tipi için kayıtlı herhangi bir doğrulayıcı varsa işleme devam et.
            // Bu kontrol, gereksiz yere doğrulama bağlamı oluşturmayı ve boş doğrulayıcı listelerini işlemeyi önler.
            if (_validators.Any())
            {
                // Doğrulama işlemi için bir bağlam (context) oluşturulur.
                // Bu bağlam, doğrulayıcılara isteğin kendisini sağlar.
                var context = new ValidationContext<TRequest>(request);

                // Tüm kayıtlı doğrulayıcıları paralel olarak çalıştırır ve doğrulama sonuçlarını toplar.
                // Task.WhenAll, tüm doğrulama görevlerinin tamamlanmasını bekler.
                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));

                // Tüm doğrulama sonuçlarından hata mesajlarını (failures) çıkarır.
                // SelectMany: Her bir doğrulama sonucundaki hataları düz bir listeye dönüştürür.
                // Where(f => f != null): Null olan hata nesnelerini filtreler (FluentValidation bazen null hata döndürebilir).
                // ToList(): Hataları bir List<ValidationFailure> olarak toplar.
                var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

                // Eğer herhangi bir doğrulama hatası bulunursa (failures listesi boş değilse).
                if (failures.Count != 0)
                {
                    // Toplanan tüm doğrulama hatalarını içeren özel bir ValidationException fırlatılır.
                    // Bu exception, API katmanında yakalanarak istemciye uygun hata mesajları döndürmek için kullanılabilir.
                    throw new ValidationException(failures);
                }
            }

            // Doğrulama başarılı olursa veya hiç doğrulayıcı yoksa, pipeline'daki bir sonraki işleyiciye (veya nihai handlera) devam et.
            // Bu, isteğin asıl iş mantığının yürütülmesini sağlar.
            return await next();
        }
    }
}
