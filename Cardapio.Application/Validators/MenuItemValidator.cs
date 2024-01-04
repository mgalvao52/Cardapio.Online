using Cardapio.Application.DTOs;
using FluentValidation;

namespace Cardapio.Application.Validators
{
    public class MenuItemValidator:AbstractValidator<AddMenuItemDTO>
    {
        public MenuItemValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Nome é obrigatorio");
            RuleFor(s => s.Price).NotEmpty().WithMessage("Preço é obrigatorio");
            RuleFor(s => s.Time).NotEmpty().WithMessage("Tempo é obrigatorio");
            RuleFor(s => s.Description).NotEmpty().WithMessage("Descrição é obrigatoria")
                .MaximumLength(300).WithMessage("Descrição não pode ter mais de 300 caracteres");
            RuleFor(s => s.Image).NotEmpty().WithMessage("Imagem é obrigatoria");
           
        }
    }
}
