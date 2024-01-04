using Cardapio.Application.DTOs;
using FluentValidation;

namespace Cardapio.Application.Validators
{
    public class OrderItemValidator:AbstractValidator<AddOrderItemDTO>
    {
        public OrderItemValidator()
        {
            RuleFor(s => s.Amount).NotEmpty().WithMessage("Quantidade é obrigatorio");
        }
    }
}
