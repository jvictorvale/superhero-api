using Microsoft.EntityFrameworkCore;
using SuperHero.Domain.Entities;
using SuperHero.Domain.Interfaces.Repositories;
using SuperHero.Infrastructure.Data.Context;

namespace SuperHero.Infrastructure.Data.Repositories;

public class SuperpoderRepository : Repository<Superpoder>, ISuperpoderRepository
{
    public SuperpoderRepository(BaseApplicationDbContext context) : base(context)
    {
    }

    public async Task Criar(Superpoder superpoder)
    {
        Context.Superpoderes.Add(superpoder);
    }

    public async Task Atualizar(Superpoder superpoder)
    {
        Context.Superpoderes.Update(superpoder);
    }

    public async Task Deletar(Superpoder superpoder)
    {
        Context.Superpoderes.Remove(superpoder);
    }

    public async Task<List<Superpoder>> ObterTodos()
    {
        return await Context.Superpoderes
            .AsNoTrackingWithIdentityResolution()
            .ToListAsync();
    }

    public async Task<Superpoder?> ObterPorId(int id)
    {
        return await Context.Superpoderes
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<List<Superpoder>> GetSuperpoderesByIdsAsync(List<int> superpoderIds)
    {
        return await Context.Superpoderes
            .Where(sp => superpoderIds.Contains(sp.Id))
            .ToListAsync();
    }

    public async Task<Superpoder?> ObterPorSuperpoder(string superpoder)
    {
        return await Context.Superpoderes
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(sp => sp.SuperPoder.ToLower() == superpoder.ToLower());
    }
}