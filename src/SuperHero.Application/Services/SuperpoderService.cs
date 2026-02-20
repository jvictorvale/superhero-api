using AutoMapper;
using SuperHero.Application.DTOs.Superpoder;
using SuperHero.Application.Interfaces;
using SuperHero.Application.Notifications;
using SuperHero.Domain.Entities;
using SuperHero.Domain.Interfaces.Repositories;

namespace SuperHero.Application.Services;

public class SuperpoderService : BaseService, ISuperpoderService
{
    private readonly ISuperpoderRepository _superpoderRepository;
    
    public SuperpoderService(
        IMapper mapper,
        INotificator notificator,
        ISuperpoderRepository superpoderRepository
        ) : base(mapper, notificator)
    {
        _superpoderRepository = superpoderRepository;
    }

    public async Task<SuperpoderDto?> Criar(SuperpoderDto superpoderDto)
    {
        var superpoderExistente =  await _superpoderRepository.ObterPorSuperpoder(superpoderDto.SuperPoder);
        if (superpoderExistente != null)
        {
            Notificator.Handle("Já existe um superpoder cadastrado com este nome.");
            return null;
        }
        
        var superpoder = Mapper.Map<Superpoder>(superpoderDto);
        _superpoderRepository.Criar(superpoder);
            
        if (await _superpoderRepository.UnitOfWork.Commit())
        {
            return Mapper.Map<SuperpoderDto>(superpoder);
        }
            
        Notificator.Handle("Não foi possível cadastrar o superpoder");
        return null;
    }

    public async Task<SuperpoderDto?> Atualizar(int id,SuperpoderDto superpoderDto)
    {
        if (id != superpoderDto.Id)
        {
            Notificator.Handle("Os ids não conferem!");
            return null;
        }

        var superpoder = await _superpoderRepository.ObterPorId(id);
        if (superpoder == null)
        {
            Notificator.HandleNotFoundResource();
            return null;
        }
        
        var superpoderExistente =  await _superpoderRepository.ObterPorSuperpoder(superpoderDto.SuperPoder);
        if (superpoderExistente != null)
        {
            Notificator.Handle("Já existe um superpoder cadastrado com este nome.");
            return null;
        }

        Mapper.Map(superpoderDto, superpoder);

        _superpoderRepository.Atualizar(superpoder);
            
        if (await _superpoderRepository.UnitOfWork.Commit())
        {
            return Mapper.Map<SuperpoderDto>(superpoder);
        }
            
        Notificator.Handle("Não foi possível alterar o superpoder");
        return null;
    }

    public async Task Deletar(int id)
    {
        if (id <= 0 || id == null)
        {
            Notificator.Handle("O id não pode ser vazio.");
            return;
        }

        var superpoder = await _superpoderRepository.ObterPorId(id);
        if (superpoder == null)
        {
            Notificator.HandleNotFoundResource();
            return;
        }

        await _superpoderRepository.Deletar(superpoder);
        
        if (await _superpoderRepository.UnitOfWork.Commit())
        {
            Notificator.Handle("Superpoder deletado com sucesso.");
        }
        else
        {
            Notificator.Handle("Erro ao tentar deletar o superpoder.");
        }
    }

    public async Task<List<SuperpoderDto>> ObterTodos()
    {
        var superpoderes = await _superpoderRepository.ObterTodos();
        if (superpoderes != null)
            return Mapper.Map<List<SuperpoderDto>>(superpoderes);
            
        Notificator.Handle("Não existe superpoder cadastrado");
        return null;
    }

    public async Task<SuperpoderDto?> ObterPorId(int id)
    {
        var superpoder = await _superpoderRepository.ObterPorId(id);
            
        if (superpoder != null)
            return Mapper.Map<SuperpoderDto>(superpoder);
            
        Notificator.HandleNotFoundResource();
        return null;
    }
}