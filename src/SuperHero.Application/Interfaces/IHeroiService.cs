using SuperHero.Application.DTOs.Heroi;

namespace SuperHero.Application.Interfaces;

public interface IHeroiService
{
    Task<HeroiDto?> Criar(AdicionarHeroiDto heroiDto);
    Task<HeroiDto> Atualizar(int id, AtualizarHeroiDto heroiDto);
    Task<bool> Deletar(int id);
    Task<List<HeroiDto>?> ObterTodos();
    Task<HeroiDto?> ObterPorId(int id);
}