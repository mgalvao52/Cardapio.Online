using Cardapio.Application.DTOs;
using FluentValidation;

namespace Cardapio.Application.Validators
{
    public class PaymentValidator: AbstractValidator<AddPaymentDTO>
    {
        public PaymentValidator()
        {
            RuleFor(s=>s.OrderId).NotEmpty().WithMessage("Numero do pedido é obrigatorio");
            RuleFor(s => s.Total).NotEmpty().WithMessage("Valor total do pagamento é obrigatorio");
        }

    }
}
