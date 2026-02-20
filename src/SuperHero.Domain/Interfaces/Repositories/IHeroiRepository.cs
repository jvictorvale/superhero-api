using SuperHero.Domain.Entities;

namespace SuperHero.Domain.Interfaces.Repositories;

public interface IHeroiRepository : IRepository<Heroi>
{
    Task Criar(Heroi heroi);
    Task Atualizar(Heroi heroi);
    Task Deletar(Heroi heroi);
    Task<List<Heroi>> ObterTodos();
    Task<Heroi?> ObterPorId(int id);
    Task<Heroi?> ObterPorNome(string nome);
    Task<Heroi?> ObterPorNomeHeroi(string nome);
}