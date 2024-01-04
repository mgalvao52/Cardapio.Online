using Cardapio.Application.DTOs;
using FluentValidation;

namespace Cardapio.Application.Validators
{
    public class OrderValidator:AbstractValidator<AddOrderDTO>
    {
        public OrderValidator()
        {
           RuleFor(s=>s.Number).NotEmpty().WithMessage("Numero é Obrigatorio");
        }
    }
}
