using FluentValidation.Results;
using SuperHero.Domain.Interfaces;
using SuperHero.Domain.Validators;

namespace SuperHero.Domain.Entities;

public class Superpoder : BaseEntity, IAggregateRoot
{
    public string SuperPoder { get; private set; }
    public string Descricao { get; private set; }
    
    public List<HeroisSuperpoderes> HeroisSuperpoderes { get; set; }

    public override bool Validar(out ValidationResult validationResult)
    {
        validationResult = new SuperpoderValidator().Validate(this);
        return validationResult.IsValid;
    }
}