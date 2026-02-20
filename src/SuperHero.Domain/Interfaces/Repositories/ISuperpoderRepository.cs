using SuperHero.Domain.Entities;

namespace SuperHero.Domain.Interfaces.Repositories;

public interface ISuperpoderRepository : IRepository<Superpoder>
{
    Task Criar(Superpoder superpoder);
    Task Atualizar(Superpoder superpoder);
    Task Deletar(Superpoder superpoder);
    Task<List<Superpoder>> ObterTodos();
    Task<Superpoder?> ObterPorId(int id);
    Task<List<Superpoder>> GetSuperpoderesByIdsAsync(List<int> superpoderIds);
    Task<Superpoder?> ObterPorSuperpoder(string superpoder);
}