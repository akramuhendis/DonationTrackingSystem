using FluentValidation;
using DonationTrackingSystem.Application.Features.Bagis.Commands;
using System;

namespace DonationTrackingSystem.Application.Validators
{
    /// <summary>
    /// DeleteBagisCommand'in doğrulama kurallarını tanımlar.
    /// </summary>
    public class DeleteBagisCommandValidator : AbstractValidator<DeleteBagisCommand>
    {
        public DeleteBagisCommandValidator()
        {
            // Id alanı için doğrulama kuralı
            RuleFor(x => x.Id)
                // Guid'in boş (Guid.Empty) olmamasını sağlar.
                .NotEmpty().WithMessage("Silinecek bağış ID'si boş olamaz.");
        }
    }
}
