using AutoMapper;
using SuperHero.Application.DTOs.Heroi;
using SuperHero.Application.DTOs.HeroisSuperpoderes;
using SuperHero.Application.Interfaces;
using SuperHero.Application.Notifications;
using SuperHero.Domain.Entities;
using SuperHero.Domain.Interfaces.Repositories;

namespace SuperHero.Application.Services;

public class HeroiService : BaseService, IHeroiService
{
    private readonly IHeroiRepository _heroiRepository;
    private readonly ISuperpoderRepository _superpoderRepository;
    
    public HeroiService(
        IMapper mapper,
        INotificator notificator,
        IHeroiRepository heroiRepository,
        ISuperpoderRepository superpoderRepository
        ) : base(mapper, notificator)
    {
        _heroiRepository = heroiRepository;
        _superpoderRepository = superpoderRepository;
    }

    public async Task<HeroiDto?> Criar(AdicionarHeroiDto heroiDto)
    {
        var nomeExistente = await _heroiRepository.ObterPorNome(heroiDto.Nome);
        if (nomeExistente != null)
        {
            Notificator.Handle("Já existe um herói cadastrado com este nome.");
            return null;
        }
        
        var heroiExistenteComMesmoNome = await _heroiRepository.ObterPorNomeHeroi(heroiDto.NomeHeroi);
    
        if (heroiExistenteComMesmoNome != null)
        {
            Notificator.Handle("Este nome de herói já está em uso por outro superherói.");
            return null;
        }
        
        var heroi = Mapper.Map<Heroi>(heroiDto);

        if (heroiDto.SuperpoderId != null && heroiDto.SuperpoderId.Any())
        {
            var superpoderes = await _superpoderRepository.GetSuperpoderesByIdsAsync(heroiDto.SuperpoderId);
            
            if (superpoderes.Count == heroiDto.SuperpoderId.Count)
            {
                heroi.HeroisSuperpoderes = superpoderes.Select(sp => new HeroisSuperpoderes
                {
                    Heroi = heroi,
                    Superpoder = sp
                }).ToList();
            }
            else
            {
                Notificator.Handle("Um ou mais superpoderes não foram encontrados.");
                return null;
            }
        }
        
        _heroiRepository.Criar(heroi);
        
        if (await _heroiRepository.UnitOfWork.Commit())
        {
            return Mapper.Map<HeroiDto>(heroi);
        }
        
        Notificator.Handle("Não foi possível cadastrar o herói");
        return null;
    }

    public async Task<HeroiDto?> Atualizar(int id, AtualizarHeroiDto heroiDto)
    {
        if (id <= 0 || id != heroiDto.Id)
        {
            Notificator.Handle("O Id fornecido é inválido ou não coincide com o herói informado.");
            return null;
        }

        var heroi = await _heroiRepository.ObterPorId(id);
        if (heroi == null)
        {
            Notificator.HandleNotFoundResource();
            return null;
        }
        
        var nomeExistente = await _heroiRepository.ObterPorNome(heroiDto.Nome);
        if (nomeExistente != null && nomeExistente.Id != id)
        {
            Notificator.Handle("Já existe um herói cadastrado com este nome.");
            return null;
        }
        
        var heroiExistenteComMesmoNome = await _heroiRepository.ObterPorNomeHeroi(heroiDto.NomeHeroi);
        if (heroiExistenteComMesmoNome != null && heroiExistenteComMesmoNome.Id != id)
        {
            Notificator.Handle("Este nome de herói já está em uso por outro superherói.");
            return null;
        }

        Mapper.Map(heroiDto, heroi);
        
        if (heroiDto.SuperpoderId != null)
        {
            heroi.HeroisSuperpoderes.Clear();

            var superpoderes = await _superpoderRepository.GetSuperpoderesByIdsAsync(heroiDto.SuperpoderId);
        
            if (superpoderes.Count == heroiDto.SuperpoderId.Count)
            {
                foreach (var sp in superpoderes)
                {
                    heroi.HeroisSuperpoderes.Add(new HeroisSuperpoderes
                    {
                        HeroiId = heroi.Id,
                        SuperpoderId = sp.Id
                    });
                }
            }
            else
            {
                Notificator.Handle("Um ou mais superpoderes não foram encontrados.");
                return null;
            }
        }

        _heroiRepository.Atualizar(heroi);
        
        if (await _heroiRepository.UnitOfWork.Commit())
        {
            return Mapper.Map<HeroiDto>(heroi);
        }
        
        Notificator.Handle("Não foi possível alterar o herói");
        return null;
    }

    public async Task<bool> Deletar(int id)
    {
        if (id <= 0 || id == null)
        {
            Notificator.Handle("O id fornecido é inválido.");
            return false;
        }

        var heroi = await _heroiRepository.ObterPorId(id);
        if (heroi == null)
        {
            Notificator.HandleNotFoundResource();
            return false;
        }

        _heroiRepository.Deletar(heroi);
        return await _heroiRepository.UnitOfWork.Commit();

    }

    public async Task<List<HeroiDto>?> ObterTodos()
    {
        var herois = await _heroiRepository.ObterTodos();
        
        if (herois == null || !herois.Any())
        {
            Notificator.Handle("Não existe herói cadastrado");
            return null;
        }
        
        return herois.Select(heroi => new HeroiDto
        {
            Id = heroi.Id,
            Nome = heroi.Nome,
            NomeHeroi = heroi.NomeHeroi,
            DataNascimento = heroi.DataNascimento,
            Altura = heroi.Altura,
            Peso = heroi.Peso,
            Superpoderes = heroi.HeroisSuperpoderes.Select(hs => new HeroisSuperpoderesDto
            {
                SuperpoderId = hs.SuperpoderId,
                SuperPoder = hs.Superpoder.SuperPoder,
                Descricao = hs.Superpoder.Descricao,
            }).ToList()
        }).ToList();
    }

    public async Task<HeroiDto?> ObterPorId(int id)
    {
        if (id <= 0)
        {
            Notificator.Handle("O Id fornecido é inválido. Deve ser um número maior que zero.");
            return null;
        }
        
        var heroi = await _heroiRepository.ObterPorId(id);
        if (heroi == null)
        {
            Notificator.HandleNotFoundResource(); 
            return null;
        }
    
        var heroiDto = Mapper.Map<HeroiDto>(heroi);
    
        heroiDto.Superpoderes = heroi.HeroisSuperpoderes?.Select(hs => new HeroisSuperpoderesDto
        {
            SuperpoderId = hs.SuperpoderId,
            SuperPoder = hs.Superpoder.SuperPoder,
            Descricao = hs.Superpoder.Descricao,
        }).ToList() ?? new List<HeroisSuperpoderesDto>();

        return heroiDto;
    }
}