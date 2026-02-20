using SuperHero.Application.DTOs.Superpoder;

namespace SuperHero.Application.Interfaces;

public interface ISuperpoderService
{
    Task<SuperpoderDto?> Criar(SuperpoderDto superpoderDto);
    Task<SuperpoderDto?> Atualizar(int id, SuperpoderDto superpoderDto);
    Task Deletar(int id);
    Task<List<SuperpoderDto>> ObterTodos();
    Task<SuperpoderDto?> ObterPorId(int id);
}