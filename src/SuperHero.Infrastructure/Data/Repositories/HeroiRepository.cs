using Microsoft.EntityFrameworkCore;
using SuperHero.Domain.Entities;
using SuperHero.Domain.Interfaces.Repositories;
using SuperHero.Infrastructure.Data.Context;

namespace SuperHero.Infrastructure.Data.Repositories;

public class HeroiRepository : Repository<Heroi>, IHeroiRepository
{
    public HeroiRepository(BaseApplicationDbContext context) : base(context)
    {
    }

    public async Task Criar(Heroi heroi)
    {
        Context.Herois.Add(heroi);
    }

    public async Task Atualizar(Heroi heroi)
    {
        Context.Herois.Update(heroi);
    }

    public async Task Deletar(Heroi heroi)
    {
        Context.Herois.Remove(heroi);
    }

    public async Task<List<Heroi>> ObterTodos()
    {
        return await Context.Herois
            .AsNoTrackingWithIdentityResolution()
            .Include(h => h.HeroisSuperpoderes)
            .ThenInclude(hs => hs.Superpoder)
            .ToListAsync();
    }

    public async Task<Heroi?> ObterPorId(int id)
    {
        return await Context.Herois
            .Include(h => h.HeroisSuperpoderes)
            .ThenInclude(hs => hs.Superpoder)
            .FirstOrDefaultAsync(u => u.Id == id);
    }
    
    public async Task<Heroi?> ObterPorNome(string nome)
    {
        return await Context.Herois
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(h => h.Nome.ToLower() == nome.ToLower());
    }
    
    public async Task<Heroi?> ObterPorNomeHeroi(string nome)
    {
        return await Context.Herois
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(h => h.NomeHeroi.ToLower() == nome.ToLower());
    }
}