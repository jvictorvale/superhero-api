using FluentValidation.Results;
using SuperHero.Domain.Interfaces;
using SuperHero.Domain.Validators;

namespace SuperHero.Domain.Entities;

public class Heroi : BaseEntity, IAggregateRoot
{
    public string Nome { get; private set; }
    public string NomeHeroi { get; private set; }
    public DateTime? DataNascimento { get; private set; }
    public float Altura { get; private set; }
    public float Peso { get; private set; }
    
    public List<HeroisSuperpoderes> HeroisSuperpoderes { get; set; }
    
    public override bool Validar(out ValidationResult validationResult)
    {
        validationResult = new HeroiValidator().Validate(this);
        return validationResult.IsValid;
    }
}