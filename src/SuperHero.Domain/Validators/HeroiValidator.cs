using FluentValidation;
using SuperHero.Domain.Entities;

namespace SuperHero.Domain.Validators;

public class HeroiValidator : AbstractValidator<Heroi>
{
    public HeroiValidator()
    {
        RuleFor(h => h.Nome)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .MaximumLength(120);
        
        RuleFor(h => h.NomeHeroi)
            .NotEmpty().WithMessage("O nome do herói é obrigatório.")
            .MaximumLength(120);
        
        RuleFor(h => h.Altura)
            .GreaterThan(0).WithMessage("A altura deve ser maior que zero.");
        
        RuleFor(h => h.Peso)
            .GreaterThan(0).WithMessage("O peso deve ser maior que zero.");
        
        RuleFor(h => h.DataNascimento)
            .LessThanOrEqualTo(DateTime.Today)
            .When(h => h.DataNascimento.HasValue)
            .WithMessage("Data de nascimento não pode ser futura.");
    }
}