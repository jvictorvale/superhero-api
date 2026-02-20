namespace SuperHero.Domain.Interfaces;

public interface IUnitOfWork
{
    Task<bool> Commit();
}