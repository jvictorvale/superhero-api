using FluentValidation;
using SuperHero.Domain.Entities;

namespace SuperHero.Domain.Validators;

public class SuperpoderValidator : AbstractValidator<Superpoder>
{
    public SuperpoderValidator()
    {
        RuleFor(s => s.SuperPoder)
            .NotEmpty().WithMessage("O nome do superpoder é obrigatório.")
            .MaximumLength(50)
            .WithMessage("O nome do superpoder deve ter no máximo 50 caracteres.");
        
        RuleFor(s => s.Descricao)
            .MaximumLength(250).WithMessage("A descrição deve ter no máximo 250 caracteres.")
            .When(s => !string.IsNullOrWhiteSpace(s.Descricao));
    }
}