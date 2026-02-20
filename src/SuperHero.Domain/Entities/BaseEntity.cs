using FluentValidation.Results;
using SuperHero.Domain.Interfaces;

namespace SuperHero.Domain.Entities;

public abstract class BaseEntity : ITracking
{
    public int Id { get; set; }
    public DateTime CriadoEm { get; set; }
    public DateTime AtualizadoEm { get; set; }

    public virtual bool Validar(out ValidationResult validationResult)
    {
        validationResult = new ValidationResult();
        return validationResult.IsValid;
    }
}